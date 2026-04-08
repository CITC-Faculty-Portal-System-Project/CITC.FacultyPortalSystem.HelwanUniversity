using Domain.Entities.IdentityModule.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Presistence.Identity
{
    public static class UserPermissionsSeederExtensions
    {
        public static async Task SeedUserPermissionsAsync(
            this IServiceProvider services,
            Guid userId,
            DateTime seedDate)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<IdentityStoreDbContext>();

            int permissionId = 1;

            var permissions = new List<UserPermission>();

            foreach (PermissionType module in Enum.GetValues(typeof(PermissionType)))
            {
                for (int i = 0; i < 4; i++)
                {
                    permissions.Add(CreatePermission(userId, permissionId++, seedDate));
                }

                if (module == PermissionType.Tickets)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        permissions.Add(CreatePermission(userId, permissionId++, seedDate));
                    }
                }
            }

            var exists = await db.UsersPermissions
                .AnyAsync(x => x.UserId == userId);

            if (!exists)
            {
                await db.UsersPermissions.AddRangeAsync(permissions);
                await db.SaveChangesAsync();
            }
        }

        private static UserPermission CreatePermission(Guid userId, int permissionId, DateTime seedDate)
        {
            return new UserPermission
            {
                UserId = userId,
                PermissionId = permissionId,
                AssignedBy = "System",
                AssignedAt = seedDate,
                AssignerId = Guid.Empty,
                CreatedBy = "System",
                CreatedAt = seedDate,
                IsDeleted = false,
                VersionNo = 1
            };
        }
    }
}