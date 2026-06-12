using Domain.Entities.IdentityModule.Authorization;
using Domain.Entities.IdentityModule.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Abstraction.Contracts.AdminModule;
using Services.Global;
using Services.Specifications.IdnetityModuleSpecifications;
using Shared.Enums.Logging;
using Shared.SpecificationParameters.IdentityModule;

namespace Services.Implementations.AdminModule
{
	public class UserManagementService(IUnitOfWork unitOfWork,
	IMapper mapper,
	IAuthenticationService authenticationService
			, UserManager<User> userManager
			, RoleManager<Role> roleManager
			, ILogger<UserManagementService> _logger)
			: BaseService<User, Guid>(unitOfWork, authenticationService, mapper),
			IUserManagementService
	{

		#region Helpers

		private async Task EnsureUserDoesNotExistAsync(UserAddDTO user)
		{
			var foundUser = await Repo.GetAsync(new UserSpecifications(user.NationalNumber));
			if (foundUser is not null)
				throw new UserAlreadyExistsException(
					$"User with national number {user.NationalNumber} already exists.");

			var checkEmail = await userManager.FindByEmailAsync(user.Email);
			if (checkEmail is not null)
				throw new UserAlreadyExistsException(
					$"User with email {user.Email} already exists.");

			var checkUserName = await userManager.FindByNameAsync(user.UserName);
			if (checkUserName is not null)
				throw new UserAlreadyExistsException(
					$"User with username {user.UserName} already exists.");
		}

		private async Task<Role> GetRoleOrThrowAsync(string roleName)
		{
			var roleRepo = UnitOfWork.GetRepository<Role, Guid>();

			return await roleRepo.GetAsync(new RoleSpecification(roleName))
				?? throw new NotFoundException($"Role with name '{roleName}' was not found.");
		}

		private async Task AddDirectPermissionsIfNeeded(
			User userEntity,
			IEnumerable<PermissionResponseDTO>? requestedPermissions,
			UserResultDto currentUser)
		{
			if (requestedPermissions is null)
				return;

			var permissionRepo = UnitOfWork.GetRepository<Permission, int>();

			userEntity.Permissions ??= new List<UserPermission>();
			userEntity.Roles ??= new List<UserRole>();

			var rolePermissionCodes = userEntity.Roles
				.Where(ur => ur.Role is not null)
				.SelectMany(ur => ur.Role!.Permissions ?? Enumerable.Empty<RolePermission>())
				.Where(rp => rp.Permission is not null && !string.IsNullOrWhiteSpace(rp.Permission.Code))
				.Select(rp => rp.Permission!.Code)
				.ToHashSet(StringComparer.OrdinalIgnoreCase);

			var userDirectPermissionCodes = userEntity.Permissions
				.Where(up => up.Permission is not null && !string.IsNullOrWhiteSpace(up.Permission.Code))
				.Select(up => up.Permission!.Code)
				.ToHashSet(StringComparer.OrdinalIgnoreCase);

			foreach (var permissionDto in requestedPermissions)
			{
				if (string.IsNullOrWhiteSpace(permissionDto.Code))
					continue;

				var code = permissionDto.Code.Trim();

				if (rolePermissionCodes.Contains(code))
					throw new UserAlreadyExistsException(
						$"Permission '{code}' is already granted to the user through one of their roles.");

				if (userDirectPermissionCodes.Contains(code))
					throw new UserAlreadyExistsException(
						$"Permission '{code}' is already directly assigned to the user.");

				var permissionEntity = await permissionRepo.GetAsync(
					new PermissionsSpecifications(code))
					?? throw new NotFoundException($"Permission with code '{code}' not found.");

				userEntity.Permissions.Add(new UserPermission
				{
					PermissionId = permissionEntity.Id,
					Permission = permissionEntity,
					UserId = userEntity.Id,
					User = userEntity,
					AssignedAt = DateTime.UtcNow,
					AssignedBy = currentUser.UserName,
					AssignerId = currentUser.UserId
				});

				userDirectPermissionCodes.Add(code);
			}
		}

		private static void ThrowIfIdentityOperationFailed(IdentityResult result)
		{
			if (result.Succeeded)
				return;

			var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
			throw new InvalidOperationException(errors);
		}

		private async Task HandleRoleSpecificCreationAsync(User userEntity, IList<string> rolesNames)
		{
			if (!rolesNames.Contains("Faculty Member"))
				return;

			var facultyMemberRepo = UnitOfWork.GetRepository<FacultyMember, Guid>();

			var existingFacultyMember = await facultyMemberRepo.GetByIdAsync(userEntity.Id);
			if (existingFacultyMember is not null)
				return;

			var facultyMember = new FacultyMember
			{
				Id = userEntity.Id,
				Name = userEntity.UserName ?? string.Empty,
				Email = userEntity.Email ?? string.Empty,
				NationalNumber = userEntity.NationalNumber ?? string.Empty
			};

			await facultyMemberRepo.AddAsync(facultyMember);
		}

		private async Task UpdateUserNameIfChangedAsync(User user, string? newUserName)
		{
			if (string.IsNullOrWhiteSpace(newUserName))
				return;

			if (!string.Equals(user.UserName, newUserName, StringComparison.Ordinal))
			{
				var result = await userManager.SetUserNameAsync(user, newUserName);
				ThrowIfIdentityOperationFailed(result);
			}
		}

		private async Task UpdateEmailIfChangedAsync(User user, string? newEmail)
		{
			if (string.IsNullOrWhiteSpace(newEmail))
				return;

			if (!string.Equals(user.Email, newEmail, StringComparison.OrdinalIgnoreCase))
			{
				var result = await userManager.SetEmailAsync(user, newEmail);
				ThrowIfIdentityOperationFailed(result);
			}
		}

		private async Task UpdatePasswordIfProvidedAsync(User user, string? newPassword)
		{
			if (string.IsNullOrWhiteSpace(newPassword))
				return;

			var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);

			var result = await userManager.ResetPasswordAsync(user, resetToken, newPassword);

			ThrowIfIdentityOperationFailed(result);

			var stampResult = await userManager.UpdateSecurityStampAsync(user);

			ThrowIfIdentityOperationFailed(stampResult);
		}

		private async Task UpdateFacultyMemberIfApplicableAsync(User user, UserEditDTO dto)
		{
			var roles = await userManager.GetRolesAsync(user);

			if (!roles.Contains("Faculty Member"))
				return;

			var facultyRepo = UnitOfWork.GetRepository<FacultyMember, Guid>();

			var facultyMember = await facultyRepo.GetAsync(
				new FacultyMemberWithIdSpcefication(user.Id)
			);

			if (facultyMember is null)
				throw new NotFoundException("Faculty member not found.");

			facultyMember.Name = dto.UserName;
			facultyMember.NationalNumber = dto.NationalNumber;
			facultyMember.Email = dto.Email;

			facultyRepo.Update(facultyMember);
		}

		private async Task EnsureUserCredentialsValidForUpdateAsync(Guid userId, UserEditDTO user)
		{
			var currentUser = await userManager.FindByIdAsync(userId.ToString());

			if (currentUser is null)
				throw new NotFoundException("User not found");

			if (currentUser.NationalNumber != user.NationalNumber)
			{
				var foundUser = await Repo.GetAsync(new UserSpecifications(user.NationalNumber));

				if (foundUser is not null)
					throw new UserAlreadyExistsException(
						$"User with national number {user.NationalNumber} already exists.");
			}

			if (currentUser.Email != user.Email)
			{
				var emailUser = await userManager.FindByEmailAsync(user.Email);

				if (emailUser is not null)
					throw new UserAlreadyExistsException(
						$"User with email {user.Email} already exists.");
			}

			if (currentUser.UserName != user.UserName)
			{
				var usernameUser = await userManager.FindByNameAsync(user.UserName);

				if (usernameUser is not null)
					throw new UserAlreadyExistsException(
						$"User with username {user.UserName} already exists.");
			}
		}

		#endregion

		protected override string EntityName => "User";

		public async Task<UserShowForAdminResponseDTO> AddUserAsync(UserAddDTO user)
		{
			var currentUser = await GetCurrentUserAsync();
			#region Log
			var userManagementLog = new LogEntry
			{
				Category = Category.UserManagement.ToString(),
				CategoryAction = CategoryAction.UserManagementActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion
			try
			{
				await EnsureUserDoesNotExistAsync(user);

				var userEntity = Mapper.Map<User>(user);

				var createResult = await userManager.CreateAsync(userEntity, user.Password);
				ThrowIfIdentityOperationFailed(createResult);

				foreach (var role in user.Roles!)
				{
					var addResult = await userManager.AddToRoleAsync(userEntity, role.Name!);
					ThrowIfIdentityOperationFailed(addResult);

				}

				await AddDirectPermissionsIfNeeded(userEntity, user.Permissions, currentUser);
				await HandleRoleSpecificCreationAsync(userEntity, user.Roles!.Select(r => r.Name).ToList());

				await UnitOfWork.SaveChangesAsync();
				var result = Mapper.Map<UserShowForAdminResponseDTO>(userEntity);
				#region Log
				userManagementLog.RenderedMessage = $"User created successfully.";
				userManagementLog.Timestamp = DateTime.Now;
				userManagementLog.Level = "Information";
				userManagementLog.AdditionalData = $"Admin: {currentUser.UserName} created a user with name : {result.UserName ?? result.Name}, id : {result.Id}, email : {result.Email}, role(s) : {result.Roles?.ToList()} was created successfully.";
				_logger.LogInformation("{@LogDetails}", userManagementLog);
				#endregion
				return result;
			}
			catch (Exception ex)
			{
				#region Log
				userManagementLog.RenderedMessage = $"Failed to create user.";
				userManagementLog.Timestamp = DateTime.Now;
				userManagementLog.Level = "Warning";
				userManagementLog.AdditionalData = $"Admin: {currentUser.UserName} failed to create user with name: {user.UserName}, national number: {user.NationalNumber}, email: {user.Email}.";
				userManagementLog.Exception = ex.ToString();
				userManagementLog.ExceptionMessage = ex.Message;
				userManagementLog.ExceptionDetail = ex.StackTrace;
				_logger.LogWarning("{@LogDetails}", userManagementLog);
				#endregion
				throw;
			}
		}

		public async Task<PaginatedResult<UserShowForAdminResponseDTO>> GetAllUsersAsync(UserSpecificationParameters parameters)
		{
			var currentUser = await GetCurrentUserAsync();
			#region Log
			var userManagementLog = new LogEntry
			{
				Category = Category.UserManagement.ToString(),
				CategoryAction = CategoryAction.UserManagementActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion
			var users = await Repo.GetAllAsync(new UserSpecifications(parameters, currentUser.UserId));

			var totalPagesCount = await Repo.CountAsync(new UserCountSpecifications(parameters, currentUser.UserId));

			var currentPage = users.Count();

			var usersResponse = Mapper.Map<IEnumerable<UserShowForAdminResponseDTO>>(users);

			#region Log
			userManagementLog.RenderedMessage = $"Admin retrieved all users successfully";
			userManagementLog.Timestamp = DateTime.Now;
			userManagementLog.Level = "Information";
			userManagementLog.AdditionalData = $"Admin: {currentUser.UserName} retrieved all [{users.Count()}] users successfully.";
			_logger.LogInformation("{@LogDetails}", userManagementLog);
			#endregion
			return new PaginatedResult<UserShowForAdminResponseDTO>(parameters.PageIndex, currentPage, totalPagesCount, usersResponse);

		}

		public async Task<UserShowForAdminResponseDTO> GetUserByIdAsync(Guid userId)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userManagementLog = new LogEntry
			{
				Category = Category.UserManagement.ToString(),
				CategoryAction = CategoryAction.UserManagementActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion

			var user = await Repo.GetAsync(new UserSpecifications(userId));
			if (user is null)
			{
				#region Log
				userManagementLog.RenderedMessage = $"Failed to retrieve user.";
				userManagementLog.Timestamp = DateTime.Now;
				userManagementLog.Level = "Warning";
				userManagementLog.AdditionalData = $"Admin: {currentUser.UserName} failed to retrieve user with id : {userId}. User not found.";
				_logger.LogWarning("{@LogDetails}", userManagementLog);
				#endregion
				throw NotFound();
			}

			#region Log
			userManagementLog.RenderedMessage = $"User retrieved successfully.";
			userManagementLog.Timestamp = DateTime.Now;
			userManagementLog.Level = "Information";
			userManagementLog.AdditionalData = $"User with id: {user.Id} was retrieved successfully.";
			_logger.LogInformation("{@LogDetails}", userManagementLog);
			#endregion
			return Mapper.Map<UserShowForAdminResponseDTO>(user);

		}

		public async Task<UserShowForAdminResponseDTO> EditUserCredeintalsAsync(UserEditDTO user, Guid userId)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userManagementLog = new LogEntry
			{
				Category = Category.UserManagement.ToString(),
				CategoryAction = CategoryAction.UserManagementActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion
			try
			{
				var foundUserEntity = await userManager.FindByIdAsync(userId.ToString())
														?? throw NotFound();
				User oldCredentials = foundUserEntity;

				await EnsureUserCredentialsValidForUpdateAsync(userId, user);

				await UpdateUserNameIfChangedAsync(foundUserEntity, user.UserName);
				await UpdateEmailIfChangedAsync(foundUserEntity, user.Email);
				await UpdatePasswordIfProvidedAsync(foundUserEntity, user.Password);

				await UpdateFacultyMemberIfApplicableAsync(foundUserEntity, user);
				foundUserEntity.NationalNumber = user.NationalNumber;

				await UnitOfWork.SaveChangesAsync();
				#region Log
				userManagementLog.RenderedMessage = $"User credentials updated successfully.";
				userManagementLog.Timestamp = DateTime.Now;
				userManagementLog.Level = "Information";
				userManagementLog.AdditionalData = $"User with id : {userId} had their credentials updated successfully by Admin: {currentUser.UserName}." +
					$" Updated credentials : [username : {(oldCredentials.UserName != foundUserEntity.UserName ? user.UserName : "No Update")}," +
					$" email : {(oldCredentials.Email != foundUserEntity.Email ? user.Email : "No Update")}," +
					$" national number : {(oldCredentials.NationalNumber != foundUserEntity.NationalNumber ? user.NationalNumber : "No Update")}," +
					$" password : {(string.IsNullOrWhiteSpace(user.Password) ? "Not Updated" : $"{user.Password}")}].";
				#endregion
				return Mapper.Map<UserShowForAdminResponseDTO>(foundUserEntity);
			}
			catch (Exception ex)
			{
				#region Log
				userManagementLog.RenderedMessage = $"Failed to edit user credentials.";
				userManagementLog.Timestamp = DateTime.Now;
				userManagementLog.Level = "warning";
				userManagementLog.AdditionalData = $"Admin: {currentUser.UserName} failed to edit [username / email / national number / password] for user with id: {userId}.";
				userManagementLog.Exception = ex.ToString();
				userManagementLog.ExceptionMessage = ex.Message;
				userManagementLog.ExceptionDetail = ex.StackTrace;
				_logger.LogWarning("{@LogDetails}", userManagementLog);
				#endregion
				throw;
			}
		}

		public async Task<UserShowForAdminResponseDTO> AssignPermissionsToUserAsync(IList<PermissionResponseDTO> permissions, Guid userId)
		{

			var currentUser = await GetCurrentUserAsync();
			#region Log
			var userPermissionsLog = new LogEntry
			{
				Category = Category.UserManagement.ToString(),
				CategoryAction = CategoryAction.UserPermissionsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion
			try
			{
				var userEntity = await Repo.GetAsync(new UserSpecifications(userId))
							?? throw NotFound();

				await AddDirectPermissionsIfNeeded(userEntity, permissions, currentUser);

				await UnitOfWork.SaveChangesAsync();
				#region Log
				userPermissionsLog.RenderedMessage = $"User assigned permissions successfully";
				userPermissionsLog.Timestamp = DateTime.Now;
				userPermissionsLog.Level = "Information";
				userPermissionsLog.AdditionalData = $"User with id: {userId} was assigned the following permission(s): [{permissions?.Select(p => p.DisplayName).ToList()}] successfully.";
				_logger.LogInformation("{@LogDetails}", userPermissionsLog);
				#endregion
				return Mapper.Map<UserShowForAdminResponseDTO>(userEntity);
			}
			catch (Exception ex)
			{
				#region Log
				userPermissionsLog.RenderedMessage = $"Failed to assign permissions to user.";
				userPermissionsLog.Timestamp = DateTime.Now;
				userPermissionsLog.Level = "Warning";
				userPermissionsLog.AdditionalData = $"Admin: {currentUser.UserName} failed to assign the following permission(s): [{permissions?.Select(p => p.DisplayName).ToList()}] to user with id: {userId}.";
				userPermissionsLog.ExceptionMessage = ex.Message;
				userPermissionsLog.Exception = ex.ToString();
				userPermissionsLog.ExceptionDetail = ex.StackTrace;
				_logger.LogWarning("{@LogDetails}", userPermissionsLog);
				#endregion
				throw;
			}
		}

		public async Task<UserShowForAdminResponseDTO> RevokePermissionsFromUserAsync
			(IList<PermissionResponseDTO> permissions, Guid userId)
		{
			var currentUser = await GetCurrentUserAsync();
			#region Log
			var userPermissionsLog = new LogEntry
			{
				Category = Category.UserManagement.ToString(),
				CategoryAction = CategoryAction.UserPermissionsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion
			var user = await Repo.GetAsync(new UserSpecifications(userId));
			if (user is null)
			{
				#region Log
				userPermissionsLog.RenderedMessage = $"User not found.";
				userPermissionsLog.Timestamp = DateTime.Now;
				userPermissionsLog.Level = "Warning";
				userPermissionsLog.AdditionalData = $"Admin: {currentUser.UserName} failed to revoke permissions from user with id: {userId} because the user was not found.";
				_logger.LogWarning("{@LogDetails}", userPermissionsLog);
				#endregion
				throw NotFound();
			}

			var now = DateTime.UtcNow;

			var requestedCodes = permissions
				.Where(p => !string.IsNullOrWhiteSpace(p.Code))
				.Select(p => p.Code.Trim())
				.ToHashSet(StringComparer.OrdinalIgnoreCase);

			var permissionsToDelete = user.Permissions!
				.Where(up =>
					up.Permission is not null &&
					!string.IsNullOrWhiteSpace(up.Permission.Code) &&
					requestedCodes.Contains(up.Permission.Code) &&
					!up.IsDeleted)
				.ToList();

			foreach (var userPermission in permissionsToDelete)
			{
				userPermission.IsDeleted = true;
				userPermission.GrantedBy = currentUser.UserName;
				userPermission.GranterId = currentUser.UserId;
				userPermission.GrantedAt = now;
				userPermission.DeletedAt = now;
				userPermission.DeletedBy = currentUser.UserName;
			}

			await UnitOfWork.SaveChangesAsync();
			#region Log
			userPermissionsLog.RenderedMessage = $"User permissions revoked successfully.";
			userPermissionsLog.Timestamp = DateTime.Now;
			userPermissionsLog.Level = "Information";
			userPermissionsLog.AdditionalData = $"User with id: {userId} was revoked the following permission(s): [{permissions?.Select(p => p.DisplayName).ToList()}] successfully.";
			_logger.LogInformation("{@LogDetails}", userPermissionsLog);
			#endregion
			return Mapper.Map<UserShowForAdminResponseDTO>(user);

		}

		public async Task<IEnumerable<PermissionResponseDTO>> GetCurrentLoggedInUserPermissionsAsync()
		{
			var currentUser = await GetCurrentUserAsync();
			#region Log
			var userPermissionsLog = new LogEntry
			{
				Category = Category.UserManagement.ToString(),
				CategoryAction = CategoryAction.UserPermissionsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion
			var userEntity = await Repo.GetAsync(new UserSpecifications(currentUser.UserId));
			if (userEntity is null)
			{
				#region Log
				userPermissionsLog.RenderedMessage = $"User not found.";
				userPermissionsLog.Timestamp = DateTime.Now;
				userPermissionsLog.Level = "Warning";
				userPermissionsLog.AdditionalData = $"Failed to get logged in user persissions because the currently logged in user has id : {currentUser.UserId} that was not found.";
				_logger.LogWarning("{@LogDetails}", userPermissionsLog);
				#endregion
				throw new NotFoundException("Current user not found.");
			}

			var directPermissions = userEntity.Permissions?
				.Where(up => up.Permission is not null)
				.Select(up => Mapper.Map<PermissionResponseDTO>(up.Permission!))
				?? Enumerable.Empty<PermissionResponseDTO>();

			var rolePermissions = userEntity.Roles?
				.Where(ur => ur.Role is not null)
				.SelectMany(ur => ur.Role!.Permissions ?? Enumerable.Empty<RolePermission>())
				.Where(rp => rp.Permission is not null)
				.Select(rp => Mapper.Map<PermissionResponseDTO>(rp.Permission!))
				?? Enumerable.Empty<PermissionResponseDTO>();

			var permissions = directPermissions
				.Concat(rolePermissions)
				.GroupBy(p => p.Code, StringComparer.OrdinalIgnoreCase)
				.Select(g => g.First())
				.ToList();

			#region Log
			userPermissionsLog.RenderedMessage = $"Current user permissions retrieved successfully.";
			userPermissionsLog.Timestamp = DateTime.Now;
			userPermissionsLog.Level = "Information";
			userPermissionsLog.AdditionalData = $"Current user with id: {currentUser.UserId} retrieved their permissions successfully. Permissions count: {permissions.Count}.";
			_logger.LogInformation("{@LogDetails}", userPermissionsLog);
			#endregion
			return permissions;
		}

		public async Task<IEnumerable<PermissionResponseDTO>> GetAllSystemPermissionsAsync(PermissionSpecificationParameters parameters)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userPermissionsLog = new LogEntry
			{
				Category = Category.UserManagement.ToString(),
				CategoryAction = CategoryAction.UserPermissionsActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion

			var permissionRepo = UnitOfWork.GetRepository<Permission, int>();

			var permissions = await permissionRepo.GetAllAsync(new PermissionsSpecifications(parameters));

			var currentPage = permissions.Count();

			#region Log
			userPermissionsLog.RenderedMessage = $"All system permissions retrieved successfully.";
			userPermissionsLog.Timestamp = DateTime.Now;
			userPermissionsLog.Level = "Information";
			userPermissionsLog.AdditionalData = $"Admin: {currentUser.UserId} retrieved all system permissions successfully. Permissions count: {permissions.Count()}.";
			_logger.LogInformation("{@LogDetails}", userPermissionsLog);
			#endregion
			return Mapper.Map<IEnumerable<PermissionResponseDTO>>(permissions);
		}

		public async Task<UserIdentifiersResposnseDTO> GetUserEmailAndIdByUsername(string username)
		{
			#region Log
			var currentUser = await GetCurrentUserAsync();
			var userManagementLog = new LogEntry
			{
				Category = Category.UserManagement.ToString(),
				CategoryAction = CategoryAction.UserManagementActions.ToString(),
				UserIP = GetUserIP(),
				UserName = currentUser.UserName,
			};
			#endregion
			var user = await userManager.FindByNameAsync(username);
			if (user is null)
			{
				#region Log
				userManagementLog.RenderedMessage = $"User not found.";
				userManagementLog.Timestamp = DateTime.Now;
				userManagementLog.Level = "Warning";
				userManagementLog.AdditionalData = $"Admin: {currentUser.UserName} failed to get user email and id from username because user with username: {username} was not found.";
				_logger.LogWarning("{@LogDetails}", userManagementLog);
				#endregion
				throw new UserNotFoundException("User Wasn't Found");
			}

			#region Log
			userManagementLog.RenderedMessage = $"User email and id retrieved successfully";
			userManagementLog.Timestamp = DateTime.Now;
			userManagementLog.Level = "Information";
			userManagementLog.AdditionalData = $"User's id: {user.Id} and email: {user.Email} were successfully retrieved from username: {username}.";
			_logger.LogInformation("{@LogDetails}", userManagementLog);
			#endregion
			return new UserIdentifiersResposnseDTO
			{
				Email = user.Email!,
				Id = user.Id,
			};
		}

	}
}
