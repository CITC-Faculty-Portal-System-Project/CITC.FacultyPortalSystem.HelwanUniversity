using Domain.Entities.IdentityModule.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Identity
{
    public static class RolePermissionSeeder
    {
        private static readonly Guid SupportAdminRoleId =
            Guid.Parse("10000000-0000-0000-0000-000000000001");

        public static async Task SeedRolePermissionsAsync(
            IdentityStoreDbContext context,
            CancellationToken ct = default)
        {
            var permissions = new[]
            {
            46, // Tickets.Read
            47, // Tickets.Update
            50, // Tickets.Reply
            54, // Tickets.ChangeStatus
            56  // Tickets.ViewAssigned
        };

            var existingPermissions = await context.RolesPermissions
                .Where(x => x.RoleId == SupportAdminRoleId)
                .Select(x => x.PermissionId)
                .ToListAsync(ct);

            var newPermissions = permissions
                .Except(existingPermissions)
                .Select(permissionId => new RolePermission
                {
                    RoleId = SupportAdminRoleId,
                    PermissionId = permissionId
                })
                .ToList();

            if (newPermissions.Count == 0)
                return;

            await context.RolesPermissions.AddRangeAsync(
                newPermissions,
                ct);

            await context.SaveChangesAsync(ct);
        }
    }
}
