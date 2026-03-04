using Domain.Entities.IdentityModule.Users;
using Microsoft.AspNetCore.Identity;

namespace Presistence.Identity
{
    public static class IdentityDbInitializer
    {
        public static async Task SeedAsync(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            CancellationToken ct = default)
        {
            string[] roles = { "SupportAdmin", "ManagementAdmin" };

            foreach (var roleName in roles)
            {
                ct.ThrowIfCancellationRequested();

                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var createRole = await roleManager.CreateAsync(new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = roleName,
                        NormalizedName = roleName.ToUpperInvariant()
                    });

                    if (!createRole.Succeeded)
                        throw new InvalidOperationException(
                            $"Failed to create role '{roleName}': {FormatErrors(createRole)}");
                }
            }

            await EnsureUserAsync(
                userManager,
                email: "TestSupportAdmin@capu.edu.eg",
                userName: "TestSupportAdmin2026",
                name: "Support Admin",
                nationalNumber: "11111111111111",
                password: "Support@123",
                role: "SupportAdmin",
                ct);

            await EnsureUserAsync(
                userManager,
                email: "TestManagementAdmin@capu.edu.eg",
                userName: "TestManagementAdmin2026",
                name: "Management Admin",
                nationalNumber: "22222222222222",
                password: "Management@123",
                role: "ManagementAdmin",
                ct);
        }

        private static async Task EnsureUserAsync(
            UserManager<User> userManager,
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

            if (user is not null)
            {
                if (!await userManager.IsInRoleAsync(user, role))
                {
                    var addToRole = await userManager.AddToRoleAsync(user, role);
                    if (!addToRole.Succeeded)
                        throw new InvalidOperationException(
                            $"Failed to add existing user '{email}' to role '{role}': {FormatErrors(addToRole)}");
                }
                return;
            }

            user = new User
            {
                Id = Guid.NewGuid(),
                UserName = userName,
                Email = email,
                Name = name,
                NationalNumber = nationalNumber,
                EmailConfirmed = true
            };

            var createUser = await userManager.CreateAsync(user, password);
            if (!createUser.Succeeded)
                throw new InvalidOperationException(
                    $"Failed to create user '{email}': {FormatErrors(createUser)}");

            var addRole = await userManager.AddToRoleAsync(user, role);
            if (!addRole.Succeeded)
                throw new InvalidOperationException(
                    $"Failed to add user '{email}' to role '{role}': {FormatErrors(addRole)}");
        }

        private static string FormatErrors(IdentityResult result)
            => string.Join(" | ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
    }
}

