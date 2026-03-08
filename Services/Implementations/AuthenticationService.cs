using Domain.Contracts;
using Domain.Entities.FacultyMemberDataModule;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Specifications.FacultyMemberDataModule;
using Shared.Dtos.Auth;
using Shared.Dtos.IdentityModule;
using Shared.Enums.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Implementations
{
	public class AuthenticationService(
		UserManager<User> _userManager,
		RoleManager<Role> _roleManager,
		ICacheService _cacheService,
		IEmailService _emailService,
		IOptions<JwtOptions> _options,
		IUnitOfWork _unitOfWork,
		IRegistrationClientService _registrationClient,
		INationalNumberPubClient _nationalNumberPubClient,
		IHttpContextAccessor _httpContextAccessor,
		ILogger<AuthenticationService> _logger
		) : IAuthenticationService
	{
		#region Helper Methods
		//GenerateSecurePassword
		private static string GenerateSecurePassword()
		{
			const int length = 12;
			const string upper = "ABCDEFGHJKLMNPQRSTUVWXYZ";
			const string lower = "abcdefghijkmnpqrstuvwxyz";
			const string digits = "23456789";
			const string symbols = "!@#$%&*?";

			var allChars = upper + lower + digits + symbols;
			using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
			var buffer = new byte[length];
			rng.GetBytes(buffer);

			var chars = new List<char>
			{
				upper[buffer[0] % upper.Length],
				lower[buffer[1] % lower.Length],
				digits[buffer[2] % digits.Length],
				symbols[buffer[3] % symbols.Length]
			};

			for (int i = 4; i < length; i++)
				chars.Add(allChars[buffer[i] % allChars.Length]);

			return new string(chars.OrderBy(_ => Guid.NewGuid()).ToArray());
		}

		//GenerateUniqueUsername
		private async Task<string> GenerateUniqueUsernameAsync(string? email, string nationalNumber)
		{
			string baseName = !string.IsNullOrWhiteSpace(email) && email.Contains("@")
				? email.Split('@')[0].ToLowerInvariant()
				: "faculty";

			var suffix = nationalNumber.Length >= 4 ? nationalNumber[^4..] : nationalNumber;
			var candidate = $"{baseName}_{suffix}";

			int counter = 0;
			while (await _userManager.FindByNameAsync(candidate) != null)
			{
				counter++;
				candidate = $"{baseName}.{suffix}{counter}";
				if (counter > 9999)
				{
					candidate = $"{baseName}_{suffix}_{Guid.NewGuid():N}".Substring(0, 16);
					break;
				}
			}

			return candidate;
		}

		//GenerateToken
		private async Task<string> CreateTokenAsync(User user)
		{
			var jwtOptions = _options.Value;

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName ?? ""),
				new Claim(ClaimTypes.Email, user.Email ?? "")
			};
			var roles = await _userManager.GetRolesAsync(user);
			foreach (var role in roles)
				claims.Add(new Claim(ClaimTypes.Role, role));

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

			var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: jwtOptions.Issuer,
				audience: jwtOptions.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddDays(jwtOptions.ExpirationInDays),
				signingCredentials: signingCredentials
				);

			return new JwtSecurityTokenHandler().WriteToken(token);

		}
		#endregion

		#region Core Methods
		//Register
		public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
		{
			var registrationLog = new LogEntry
			{
				Category = Category.Authentication.ToString(),
				CategoryAction = CategoryAction.UserRegistration.ToString(),
			};

			// Call External Microservice here to validate NtionalNumber & Get Email
			var externalUser = await GetUserInfoFromExternalService(registerDto.NationalNumber);
			var email = externalUser.Email.Trim().ToLowerInvariant();

			//Check if user already exists
			var existingUser = await _userManager.FindByEmailAsync(email);
			if (existingUser is not null)
			{
				#region Log
				registrationLog.Timestamp = DateTime.Now;
				registrationLog.Level = "Warning";
				registrationLog.RenderedMessage = $"Registration Failed [User Already Exists].";
				registrationLog.UserIP = "REQUEST_IP";
				registrationLog.AdditionalData = $"User with National Number {registerDto.NationalNumber} and Email {email} already exists in the database";
				_logger.LogWarning("{@LogDetails}", registrationLog);
				#endregion
				throw new UserAlreadyExistsException();
			}

			//Create Credentials
			var username = await GenerateUniqueUsernameAsync(email, registerDto.NationalNumber);
			var password = GenerateSecurePassword();

			var newUser = new User
			{
				UserName = username,
				Email = email,
				NationalNumber = externalUser.NationalNumber
			};


			var secification = new FacultyMemberWithEmailSpecifications(email);
			var facultyMemberRepo = _unitOfWork.GetRepository<FacultyMember, Guid>();
			var member = await facultyMemberRepo.GetAsync(secification);
			if (member is not null)
			{
				#region Log
				registrationLog.Timestamp = DateTime.Now;
				registrationLog.Level = "Warning";
				registrationLog.RenderedMessage = $"Registration Failed [User Already Exists].";
				registrationLog.UserIP = "REQUEST_IP";
				registrationLog.AdditionalData = $"User with National Number {registerDto.NationalNumber} and Email {email} already exists in the database";
				_logger.LogWarning("{@LogDetails}", registrationLog);
				#endregion
				throw new UserAlreadyExistsException("This Member is Already Registered");
			}


			var result = await _userManager.CreateAsync(newUser, password);
			if (!result.Succeeded)
			{
				#region Log
				registrationLog.Timestamp = DateTime.Now;
				registrationLog.Level = "Error";
				registrationLog.RenderedMessage = "Failed to Register User into the database";
				registrationLog.UserIP = "REQUEST_IP";
				registrationLog.AdditionalData = $"User with National Number {registerDto.NationalNumber} and Email {email} failed to be Added to the database";
				registrationLog.Exception = string.Join("- ", result.Errors.Select(e => e.Description));
				registrationLog.ExceptionMessage = "Failed to Register User into the database";
				_logger.LogError("{@LogDetails}", registrationLog);
				#endregion
				var errors = result.Errors.Select(e => e.Description).ToList();
				throw new ValidationException(errors);
			}

			var roleName = "Faculty Member";
			if (!await _roleManager.RoleExistsAsync(roleName))
			{
				await _roleManager.CreateAsync(new Role { Name = roleName, NormalizedName = roleName.ToUpperInvariant() });
			}
			await _userManager.AddToRoleAsync(newUser, roleName);

			//Cache Credentials
			await _cacheService.SetCachedValueAsync($"auth:email:{newUser.Id}", email, TimeSpan.FromMinutes(30));
			await _cacheService.SetCachedValueAsync($"auth:username:{newUser.Id}", username, TimeSpan.FromMinutes(30));
			await _cacheService.SetCachedValueAsync($"auth:password:{newUser.Id}", password, TimeSpan.FromMinutes(30));

			//Send Credentials Email
			if (!string.IsNullOrEmpty(email))
			{
				#region Log
				registrationLog.Timestamp = DateTime.Now;
				registrationLog.Level = "Information";
				registrationLog.RenderedMessage = "User Registered Successfully.";
				registrationLog.UserIP = "REQUEST_IP";
				registrationLog.AdditionalData = $"User with National Number {registerDto.NationalNumber} and Email {email} was registered successfully.";
				_logger.LogInformation("{@LogDetails}", registrationLog);
				#endregion
				await _emailService.SendCredentialsAsync(newUser.Id, username, password);
			}

			var facultyMember = new FacultyMember
			{
				Id = newUser.Id,  // exact same Id
				Name = newUser.UserName ?? "",
				Email = newUser.Email ?? "",
				NationalNumber = newUser.NationalNumber ?? ""
			};

			await facultyMemberRepo.AddAsync(facultyMember);
			await _unitOfWork.SaveChangesAsync();


			await _nationalNumberPubClient.PublishUserNationalNumberAsync(registerDto.NationalNumber);

			return new UserResultDto(UserName: newUser.UserName ?? "", newUser.Email ?? "");
		}

		//Login
		public async Task<LoginClaims> LoginAsync(LoginDto loginDto)
		{
			var LoginLog = new LogEntry
			{
				Category = Category.Authentication.ToString(),
				CategoryAction = CategoryAction.UserLogin.ToString(),
			};

			var user = await _userManager.FindByNameAsync(loginDto.Username);
			if (user is null)
			{
				#region Log
				LoginLog.Timestamp = DateTime.Now;
				LoginLog.Level = "Warning";
				LoginLog.RenderedMessage = "Login Failed [User Not Found].";
				LoginLog.UserIP = "REQUEST_IP";
				LoginLog.AdditionalData = $"Login attempt with Username {loginDto.Username} and Password {loginDto.Password} failed because the user was not found in the database.";
				_logger.LogWarning("{@LogDetails}", LoginLog);
				#endregion
				throw new UnauthorizedException();
			}

			var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
			var role = await _userManager.GetRolesAsync(user);
			if (!result)
			{
				#region Log
				LoginLog.Timestamp = DateTime.Now;
				LoginLog.Level = "Warning";
				LoginLog.RenderedMessage = "Login Failed [User Not Found].";
				LoginLog.UserIP = "REQUEST_IP";
				LoginLog.AdditionalData = $"Login attempt with Username {loginDto.Username} and Password {loginDto.Password} failed because the password is incorrect.";
				_logger.LogWarning("{@LogDetails}", LoginLog);
				#endregion
				throw new UnauthorizedException();
			}

			var token = await CreateTokenAsync(user);
			var response = new LoginClaims
			{
				Email = user.Email,
				Role = role.FirstOrDefault(),
				UserName = user.UserName,
				Token = token
			};
			#region Log
			LoginLog.Timestamp = DateTime.Now;
			LoginLog.Level = "Information";
			LoginLog.RenderedMessage = "Login Successful";
			LoginLog.UserIP = "REQUEST_IP";
			LoginLog.UserName = "REQUEST_USER";
			LoginLog.AdditionalData = $"User with Username {loginDto.Username} and Role {role.FirstOrDefault()} logged in successfully.";
			_logger.LogInformation("{@LogDetails}", LoginLog);
			#endregion
			return (response);
		}

		//CheckEmail
		public async Task<bool> CheckEmailExistAsync(string userEmail)
		{
			var user = await _userManager.FindByEmailAsync(userEmail);
			return user != null;
		}

		//ConfirmOTP
		public async Task ConfirmEmail(string userEmail)
		{
			var checkEmailLog = new LogEntry
			{
				Category = Category.Authentication.ToString(),
				CategoryAction = CategoryAction.SendOTP.ToString(),
			};

			var checkEmail = await CheckEmailExistAsync(userEmail);
			if (!checkEmail)
			{
				#region Log
				checkEmailLog.Timestamp = DateTime.Now;
				checkEmailLog.Level = "Warning";
				checkEmailLog.RenderedMessage = "OTP Send Failed [User Not Found].";
				checkEmailLog.UserIP = "REQUEST_IP";
				checkEmailLog.AdditionalData = $"Attempt to send OTP to {userEmail} failed because the email was not found in the database.";
				_logger.LogWarning("{@LogDetails}", checkEmailLog);
				#endregion
				throw new UserNotFoundException(userEmail);
			}
			await _emailService.SendOTPAsync(userEmail);
		}

		//VerifyOTP
		public async Task<bool> VerifyOTPAsync(OTPSendDTO otpSendDto)
		{
			var otpKey = $"auth:otp:{otpSendDto.Email.ToLower()}";
			var cachedOTP = await _cacheService.GetCachedValueAsync(otpKey);

			if (string.IsNullOrWhiteSpace(cachedOTP) || string.IsNullOrWhiteSpace(otpSendDto.Otp))
				return false;

			return string.Equals(cachedOTP, otpSendDto.Otp, StringComparison.Ordinal);
		}

		//ResetPassowrd
		public async Task<bool> ResetPasswordAsync(ResetPasswordDto passwordDto)
		{
			var resetPasswordLog = new LogEntry
			{
				Category = Category.Authentication.ToString(),
				CategoryAction = CategoryAction.ResetPassword.ToString()
			};

			var email = await _cacheService.GetCachedValueAsync($"auth:email:{passwordDto.Email.ToLower()}") ?? "";
			var user = await _userManager.FindByEmailAsync(passwordDto.Email ?? "");
			if (user is null) return false;

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			var result = await _userManager.ResetPasswordAsync(user, token, passwordDto.NewPassword);
			bool isSuccess = result.Succeeded;
			if (isSuccess)
			{
				#region Log
				resetPasswordLog.Timestamp = DateTime.Now;
				resetPasswordLog.Level = "Information";
				resetPasswordLog.RenderedMessage = "Password Reset Successfully";
				resetPasswordLog.UserIP = "REQUEST_IP";
				resetPasswordLog.AdditionalData = $"Password for user with Email {passwordDto.Email} was reset successfully to {passwordDto.NewPassword}.";
				_logger.LogInformation("{@LogDetails}", resetPasswordLog);
				#endregion
				return true;
			}
			return false;
		}

		//GetUserInfoFromExternalService
		public async Task<UserRegistrationClientDto> GetUserInfoFromExternalService(string nationalNumber)
		{
			var clientLog = new LogEntry
			{
				Category = Category.Authentication.ToString(),
				CategoryAction = CategoryAction.CheckNationalNumber.ToString(),
			};
			var user = await _registrationClient.CheckNationalNumber(nationalNumber);
			if (user is null || string.IsNullOrWhiteSpace(user.NationalNumber))
			{
				#region Log
				clientLog.Timestamp = DateTime.Now;
				clientLog.Level = "Warning";
				clientLog.RenderedMessage = $"User with National Number {nationalNumber} not Found.";
				clientLog.AdditionalData = $"The external system did not return valid data for national number: {nationalNumber}. This may indicate that the national number is invalid or not registered.";
				clientLog.UserIP = "REQUEST_IP";
				_logger.LogWarning("{@LogDetails}", clientLog);
				#endregion
				throw new NotFoundException($"User with National Number {nationalNumber} not Found.");
			}
			return new UserRegistrationClientDto
			{
				Exists = true,
				NationalNumber = user.NationalNumber,
				Email = user.Email
			};
		}

		//GetCurrentUser
		public async Task<UserResultDto> GetCurrentUserAsync(string userEmail)
		{
			var user = await _userManager.FindByEmailAsync(userEmail)
				?? throw new UserNotFoundException(userEmail);
			return new UserResultDto(UserName: user.UserName ?? "", user.Email ?? "",
				user.Id);
		}

		public string GetLoggedUserEmail()
		{
			var user = _httpContextAccessor.HttpContext?.User;
			var email = user.FindFirst(ClaimTypes.Email)?.Value.ToString();

			return email;

		}
		#endregion
	}
}
