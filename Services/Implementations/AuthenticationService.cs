using Domain.Contracts;
using Domain.Entities.FacultyMemberDataModule;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Specifications.FacultyMemberDataModule;
using Shared.Dtos.Auth;
using Shared.Dtos.IdentityModule;
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
        IHttpContextAccessor _httpContextAccessor
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
            // Call External Microservice here to validate NtionalNumber & Get Email
            var externalUser = await GetUserInfoFromExternalService(registerDto.NationalNumber);
            var email = externalUser.Email.Trim().ToLowerInvariant();

            //Check if user already exists
            var existingUser = await _userManager.FindByEmailAsync(email);
            if(existingUser is not null) 
                throw new UserAlreadyExistsException();

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
                throw new UserAlreadyExistsException("This Member is Already Registered");



            var result = await _userManager.CreateAsync(newUser, password);
            if (!result.Succeeded)
            {
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
            await _cacheService.SetCachedValueAsync($"auth:username:{newUser.Id}",username,TimeSpan.FromMinutes(30));
            await _cacheService.SetCachedValueAsync($"auth:password:{newUser.Id}",password,TimeSpan.FromMinutes(30));

            //Send Credentials Email
            if (!string.IsNullOrEmpty(email))
            {
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

            return new UserResultDto(UserName: newUser.UserName ?? "" , newUser.Email ?? "");
        }

        //Login
        public async Task<LoginClaims> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user is null) throw new UnauthorizedException();

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            var role = await _userManager.GetRolesAsync(user);
            if (!result) throw new UnauthorizedException();


            var token = await CreateTokenAsync(user);
            var response = new LoginClaims
            {
                Email = user.Email!,
                Role = role.FirstOrDefault()!,
                UserName = user.UserName!,
                Token = token,
                NationalNumber = user.NationalNumber
            };
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
            var checkEmail = await CheckEmailExistAsync(userEmail);
            if (!checkEmail) throw new UserNotFoundException(userEmail);
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
            var email = await _cacheService.GetCachedValueAsync($"auth:email:{passwordDto.Email.ToLower()}") ?? "";
            var user = await _userManager.FindByEmailAsync(passwordDto.Email ?? "");
            if (user is null) return false;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, passwordDto.NewPassword);

            return result.Succeeded;
        }

        //GetUserInfoFromExternalService
        public async Task<UserRegistrationClientDto> GetUserInfoFromExternalService(string nationalNumber)
        {
            var user = await _registrationClient.CheckNationalNumber(nationalNumber);
            if (user is null || string.IsNullOrWhiteSpace(user.NationalNumber))
                throw new NotFoundException("errors.NationalNumber.notFound");
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
            return new UserResultDto(UserName: user.UserName ?? "", user.Email ?? "" , 
                user.Id);
        }

        public string GetLoggedUserEmail()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var email = user!.FindFirst(ClaimTypes.Email)?.Value.ToString();

            return email!;

        }

        #endregion
    }
}
