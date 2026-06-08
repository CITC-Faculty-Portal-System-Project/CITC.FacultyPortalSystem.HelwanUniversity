using Domain.Entities.IdentityModule.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Presistence.Identity
{
    public static class IdentityDbInitializerExtensions
    {
        public static async Task UseIdentityDatabaseInitializerAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var dbContext = services.GetRequiredService<IdentityStoreDbContext>();

            await dbContext.Database.MigrateAsync();

            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<Role>>();

            await IdentityDbInitializer.SeedAsync(userManager, roleManager);
        }
    }
}