//using Domain.Entities.IdentityModule.Users;
//using Microsoft.AspNetCore.Identity;
//using Services.Abstraction.Contracts.IdentityModule;

//namespace Services.Implementations.IdnetityModule
//{
//    public class PermissionService(UserManager<User> _userManager 
//                                    , RoleManager<Role> _roleManager) : IPermissionService
//    {
//        public async Task<IReadOnlyList<string>> GetEffectivePermissionsAsync(Guid userId, CancellationToken ct = default)
//        {
//            var userOverride = await _userPermissionRead.GetOverrideAsync(userId, permissionCode, ct);
//            if (userOverride is not null)
//                return userOverride.IsGranted; // deny overrides allow

//            // 2) role-based permissions
//            var roleIds = await GetUserRoleIdsAsync(userId, ct);
//            if (roleIds.Count == 0) return false;

//            return await _rolePermissionRead.AnyRoleHasPermissionAsync(roleIds, permissionCode, ct);
//        }

//        public async Task<bool> HasPermissionAsync
//            (Guid userId, string permissionCode, CancellationToken ct = default)
//        {
//            var userOverride = await _userManager.FindByIdAsync(userId.ToString());.GetOverrideAsync(userId, permissionCode, ct);
//            if (userOverride is not null)
//                return userOverride.IsGranted; // deny overrides allow

//            // 2) role-based permissions
//            var roleIds = await GetUserRoleIdsAsync(userId, ct);
//            if (roleIds.Count == 0) return false;

//            return await _rolePermissionRead.AnyRoleHasPermissionAsync(roleIds, permissionCode, ct);
//        }
//    }
//}
