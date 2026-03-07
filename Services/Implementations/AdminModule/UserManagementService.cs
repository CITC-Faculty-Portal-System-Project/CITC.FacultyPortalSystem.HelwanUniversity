using Domain.Entities.IdentityModule.Authorization;
using Domain.Entities.IdentityModule.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Abstraction.Contracts.AdminModule;
using Services.Global;
using Services.Specifications.IdnetityModuleSpecifications;

using Shared.SpecificationParameters.IdentityModule;

namespace Services.Implementations.AdminModule
{
    public class UserManagementService(IUnitOfWork unitOfWork,
    IMapper mapper,
    IAuthenticationService authenticationService 
            , UserManager<User> userManager 
            , RoleManager<Role> roleManager)
            :BaseService<User, Guid>(unitOfWork, authenticationService, mapper), 
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

        private async Task HandleRoleSpecificCreationAsync(User userEntity, string roleName)
        {
            if (!string.Equals(roleName, "Faculty Member", StringComparison.OrdinalIgnoreCase))
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

        #endregion

        protected override string EntityName => "User";

        public async Task<UserShowForAdminResponseDTO> AddUserAsync(UserAddDTO user)
        {
            await EnsureUserDoesNotExistAsync(user);

            var currentUser = await GetCurrentUserAsync();
            
            var roleEntity = await GetRoleOrThrowAsync(user!.Role!.Name);

            var userEntity = Mapper.Map<User>(user);

            var createResult = await userManager.CreateAsync(userEntity, user.Password);
            ThrowIfIdentityOperationFailed(createResult);

            var addToRoleResult = await userManager.AddToRoleAsync(userEntity, user.Role.Name);
            ThrowIfIdentityOperationFailed(addToRoleResult);

            await AddDirectPermissionsIfNeeded(userEntity, user.Permissions, currentUser);
            await HandleRoleSpecificCreationAsync(userEntity, user.Role.Name);

            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<UserShowForAdminResponseDTO>(userEntity);
        }

        public async Task<PaginatedResult<UserShowForAdminResponseDTO>> GetAllUsersAsync(UserSpecificationParameters parameters)
        {
            var currentUser = await GetCurrentUserAsync();
            var users = await Repo.GetAllAsync(new UserSpecifications(parameters , currentUser.UserId));

            var totalPagesCount = await Repo.CountAsync(new UserSpecifications(parameters, currentUser.UserId));

            var currentPage = users.Count();

            var usersResponse = Mapper.Map<IEnumerable<UserShowForAdminResponseDTO>>(users);

            return new PaginatedResult<UserShowForAdminResponseDTO>(parameters.PageIndex, currentPage, totalPagesCount, usersResponse);

        }

        public async Task<UserShowForAdminResponseDTO> GetUserByIdAsync(Guid userId)
        {
            var user = await Repo.GetAsync(new UserSpecifications(userId))
                ?? throw NotFound();

            return Mapper.Map<UserShowForAdminResponseDTO>(user);

        }

        public async Task<UserShowForAdminResponseDTO> EditUserCredeintalsAsync(UserEditDTO user , Guid userId)
        {
            var foundUserEntity = await userManager.FindByIdAsync(userId.ToString())
                                                ?? throw NotFound();

            await EnsureUserDoesNotExistAsync(Mapper.Map<UserAddDTO>(user));

            await UpdateUserNameIfChangedAsync(foundUserEntity, user.UserName);
            await UpdateEmailIfChangedAsync(foundUserEntity, user.Email);
            await UpdatePasswordIfProvidedAsync(foundUserEntity , user.Password);

            await UpdateFacultyMemberIfApplicableAsync(foundUserEntity, user);
            foundUserEntity.NationalNumber = user.NationalNumber;

            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<UserShowForAdminResponseDTO>(foundUserEntity);
        }

        public async Task<UserShowForAdminResponseDTO> AssignPermissionsToUserAsync(IList<PermissionResponseDTO> permissions, Guid userId)
        {

            var currentUser = await GetCurrentUserAsync();

            var userEntity = await Repo.GetAsync(new UserSpecifications(userId))
                        ?? throw NotFound();


            await AddDirectPermissionsIfNeeded(userEntity, permissions, currentUser);

            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<UserShowForAdminResponseDTO>(userEntity);

        }

        public async Task<UserShowForAdminResponseDTO> RevokePermissionsFromUserAsync
            (IList<PermissionResponseDTO> permissions, Guid userId)
        {

            var currentUser = await GetCurrentUserAsync();

            var user = await Repo.GetAsync(new UserSpecifications(userId))
                      ?? throw NotFound();

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

            return Mapper.Map<UserShowForAdminResponseDTO>(user);

        }

        public async Task<IEnumerable<PermissionResponseDTO>> GetCurrentLoggedInUserPermissionsAsync()
        {
            var currentUser = await GetCurrentUserAsync();

            var userEntity = await Repo.GetAsync(new UserSpecifications(currentUser.UserId))
                ?? throw new NotFoundException("Current user not found.");

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

            return permissions;
        }

        public async Task<PaginatedResult<PermissionResponseDTO>> GetAllSystemPermissionsAsync(PermissionSpecificationParameters parameters)
        {
            var permissionRepo = UnitOfWork.GetRepository<Permission, int>();
            
            var permissions = await permissionRepo.GetAllAsync(new PermissionsSpecifications(parameters));

            var totalPagesCount = await permissionRepo.CountAsync(new PermissionsCountSpecification(parameters));

            var currentPage = permissions.Count();

            var permissionsResponse = Mapper.Map<IEnumerable<PermissionResponseDTO>>(permissions);

            return new PaginatedResult<PermissionResponseDTO>(parameters.PageIndex, currentPage, totalPagesCount, permissionsResponse);
        }
    }
}
