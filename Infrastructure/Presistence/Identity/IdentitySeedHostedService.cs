using Domain.Entities.IdentityModule.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Presistence.Identity;

namespace Presistence.Identity
{
    public sealed class IdentitySeedHostedService
            (IServiceProvider _sp
            , ILogger<IdentitySeedHostedService> _logger) 
            : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                using var scope = _sp.CreateScope();

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                await IdentityDbInitializer.SeedAsync(userManager, roleManager, cancellationToken);

                _logger.LogInformation("Identity seeding completed.");
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Identity seeding canceled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Identity seeding failed.");
             
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}