using Domain.Entities.IdentityModule.Users;

namespace Services.Helpers.TicketingModuleHelpers
{
    public static class PermissionHelper
    {
        public static HashSet<string> GetAllPermissionCodes(User user)
        {
            var directPermissions = user.Permissions?
                .Where(up => up.Permission is not null)
                .Select(up => up.Permission!.Code)
                ?? Enumerable.Empty<string>();

            var rolePermissions = user.Roles?
                .Where(ur => ur.Role is not null)
                .SelectMany(ur => ur.Role!.Permissions ?? [])
                .Where(rp => rp.Permission is not null)
                .Select(rp => rp.Permission!.Code)
                ?? Enumerable.Empty<string>();

            return directPermissions
                .Concat(rolePermissions)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
        }
    }
}
