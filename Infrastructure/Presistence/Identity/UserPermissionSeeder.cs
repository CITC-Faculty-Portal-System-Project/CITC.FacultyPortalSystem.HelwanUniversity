using Domain.Entities.IdentityModule.Authorization;

namespace Presistence.Identity
{
    public static class UserPermissionSeeder
    {
        private static readonly Guid ManagementAdminUserId =
            Guid.Parse("A9923638-8866-4A89-A9FE-9CF329CFC8F7");

        public static async Task SeedUserPermissionsAsync(
            IdentityStoreDbContext context,
            CancellationToken ct = default)
        {
            var seedDate = new DateTime(
                2025,
                1,
                1,
                0,
                0,
                0,
                DateTimeKind.Utc);

            var existingPermissions = await context.UsersPermissions
                .Where(x => x.UserId == ManagementAdminUserId)
                .Select(x => x.PermissionId)
                .ToListAsync(ct);

            var allPermissionIds = await context.Permissions
                .Select(x => x.Id)
                .ToListAsync(ct);

            var permissionsToAdd = allPermissionIds
                .Except(existingPermissions)
                .Select(permissionId => new UserPermission
                {
                    UserId = ManagementAdminUserId,
                    PermissionId = permissionId,
                    AssignedBy = "System",
                    AssignedAt = seedDate,
                    AssignerId = Guid.Empty,
                    CreatedBy = "System",
                    CreatedAt = seedDate,
                    IsDeleted = false,
                    VersionNo = 1
                })
                .ToList();

            if (permissionsToAdd.Count == 0)
                return;

            await context.UsersPermissions.AddRangeAsync(
                permissionsToAdd,
                ct);

            await context.SaveChangesAsync(ct);
        }
    }
}
