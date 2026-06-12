using Domain.Entities.IdentityModule.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Presistence.Identity.Seeding
{
    public static class RolePermissionsSeeder
    {
        public static async Task SeedRolePermissionsAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<IdentityStoreDbContext>();

            var roleId = new Guid("10000000-0000-0000-0000-000000000001");

            var permissions = new List<RolePermission>
            {
                new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = 46
                },
                new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = 47
                },
                new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = 50
                },
                new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = 54
                },
                new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = 56
                }
            };

            var exists = await db.RolesPermissions
                .AnyAsync(x => x.RoleId == roleId);

            if (!exists)
            {
                await db.RolesPermissions.AddRangeAsync(permissions);
                await db.SaveChangesAsync();
            }
        }
    }
}