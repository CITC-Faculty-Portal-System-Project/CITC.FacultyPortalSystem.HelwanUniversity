using Domain.Entities.IdentityModule.Authorization;
using Domain.Entities.IdentityModule.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Abstraction.Contracts.IdentityModule;
using Services.Specifications.IdnetityModuleSpecifications;

namespace Services.Implementations.IdnetityModule
{
    public sealed class PermissionService(UserManager<User> _userManager 
                    , IAuthenticationService _authenticationService
                    , IUnitOfWork _unitOfWork
                    , RoleManager<Role> _roleManager) : IPermissionService
    {
    
        public async Task<IReadOnlyList<string>> GetEffectivePermissionsAsync()
        {
            
            var user = await _authenticationService.GetCurrentUserAsync(_authenticationService.GetLoggedUserEmail());

            var permissionRepo = _unitOfWork.GetRepository<Permission, int>();
            
            var userPerms = await permissionRepo.GetAllAsync(new UserPermissionsSpecifications(user.UserId));


            var identityUser = await _userManager.FindByIdAsync(user.UserId.ToString());
            if (identityUser is null) return [];

            var roleNames = await _userManager.GetRolesAsync(identityUser); 
            if (roleNames.Count == 0)
                return userPerms.Select(p => p.Code).Distinct().ToList();

            var roleIds = await _roleManager.Roles
                                .Where(r => roleNames.Contains(r.Name!))
                                .Select(r => r.Id)
                                .ToListAsync();


            var rolePerms = roleIds.Count == 0
                ? new List<Permission>()
                : (await permissionRepo.GetAllAsync(new RolePermissionsSpecifications(roleIds)));

            return userPerms
                .Select(p => p.Code)
                .Concat(rolePerms.Select(p => p.Code))
                .Where(code => !string.IsNullOrWhiteSpace(code))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        public async Task<bool> HasPermissionAsync(string permissionCode)
        {
            if (string.IsNullOrWhiteSpace(permissionCode)) return false;

            var permissions = await GetEffectivePermissionsAsync();
            return permissions.Contains(permissionCode, StringComparer.OrdinalIgnoreCase);
        }
    }
}
