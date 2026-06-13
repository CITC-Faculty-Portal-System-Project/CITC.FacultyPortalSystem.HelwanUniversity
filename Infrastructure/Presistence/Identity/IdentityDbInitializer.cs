using Domain.Entities.IdentityModule.Users;
using Microsoft.AspNetCore.Identity;
using Presistence.Identity.Seeding;

namespace Presistence.Identity
{
    public static class IdentityDbInitializer
    {
        private static readonly Guid SupportAdminRoleId =
            Guid.Parse("10000000-0000-0000-0000-000000000001");

        private static readonly Guid ManagementAdminRoleId =
            Guid.Parse("10000000-0000-0000-0000-000000000002");

        private static readonly Guid SupportAdminUserId =
            Guid.Parse("C24E082C-244B-49D1-A2D9-39A994DC77E5");

        private static readonly Guid ManagementAdminUserId =
            Guid.Parse("A9923638-8866-4A89-A9FE-9CF329CFC8F7");

        public static async Task SeedAsync(
            IdentityStoreDbContext context,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            CancellationToken ct = default)
        {
            await EnsureRoleAsync(
                roleManager,
                SupportAdminRoleId,
                "SupportAdmin",
                ct);

            await EnsureRoleAsync(
                roleManager,
                ManagementAdminRoleId,
                "ManagementAdmin",
                ct);

            await EnsureUserAsync(
                userManager,
                id: SupportAdminUserId,
                email: "TestSupportAdmin@capu.edu.eg",
                userName: "TestSupportAdmin2026",
                name: "Support Admin",
                nationalNumber: "11111111111111",
                password: "Support@123",
                role: "SupportAdmin",
                ct: ct);

            await EnsureUserAsync(
                userManager,
                id: ManagementAdminUserId,
                email: "TestManagementAdmin@capu.edu.eg",
                userName: "TestManagementAdmin2026",
                name: "Management Admin",
                nationalNumber: "22222222222222",
                password: "Management@123",
                role: "ManagementAdmin",
                ct: ct);

            await RolePermissionSeeder.SeedRolePermissionsAsync(context, ct);

            await UserPermissionSeeder.SeedUserPermissionsAsync(context, ct);
        }

        private static async Task EnsureRoleAsync(
            RoleManager<Role> roleManager,
            Guid id,
            string roleName,
            CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var existingRole = await roleManager.FindByNameAsync(roleName);

            if (existingRole is not null)
                return;

            var createRole = await roleManager.CreateAsync(new Role
            {
                Id = id,
                Name = roleName,
                NormalizedName = roleName.ToUpperInvariant()
            });

            if (!createRole.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Failed to create role '{roleName}': {FormatErrors(createRole)}");
            }
        }

        private static async Task EnsureUserAsync(
            UserManager<User> userManager,
            Guid id,
            string email,
            string userName,
            string name,
            string nationalNumber,
            string password,
            string role,
            CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
            {
                user = new User
                {
                    Id = id,
                    UserName = userName,
                    Email = email,
                    Name = name,
                    NationalNumber = nationalNumber,
                    EmailConfirmed = true
                };

                var createUser = await userManager.CreateAsync(user, password);

                if (!createUser.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"Failed to create user '{email}': {FormatErrors(createUser)}");
                }
            }
            else
            {
                var shouldUpdate = false;

                if (user.Id != id)
                {
                    throw new InvalidOperationException(
                        $"User '{email}' already exists with different Id '{user.Id}'. Expected fixed Id '{id}'.");
                }

                if (user.UserName != userName)
                {
                    user.UserName = userName;
                    shouldUpdate = true;
                }

                if (user.Name != name)
                {
                    user.Name = name;
                    shouldUpdate = true;
                }

                if (user.NationalNumber != nationalNumber)
                {
                    user.NationalNumber = nationalNumber;
                    shouldUpdate = true;
                }

                if (!user.EmailConfirmed)
                {
                    user.EmailConfirmed = true;
                    shouldUpdate = true;
                }

                if (shouldUpdate)
                {
                    var updateUser = await userManager.UpdateAsync(user);

                    if (!updateUser.Succeeded)
                    {
                        throw new InvalidOperationException(
                            $"Failed to update existing user '{email}': {FormatErrors(updateUser)}");
                    }
                }
            }

            if (!await userManager.IsInRoleAsync(user, role))
            {
                var addToRole = await userManager.AddToRoleAsync(user, role);

                if (!addToRole.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"Failed to add user '{email}' to role '{role}': {FormatErrors(addToRole)}");
                }
            }
        }

        private static string FormatErrors(IdentityResult result)
            => string.Join(" | ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));


    }
}