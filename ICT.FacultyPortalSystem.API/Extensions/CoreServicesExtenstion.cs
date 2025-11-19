using Shared.Common;

namespace ICIT.FacultyPortalSystem.API.Extensions
{
    public static class CoreServicesExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(cfg => { }, typeof(AssemblyReference).Assembly);
            services.AddScoped<IServiceManager, ServiceManager>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<Func<IAuthenticationService>>(provider =>
            () => provider.GetRequiredService<IAuthenticationService>()
            );

            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<Func<ICacheService>>(provider =>
            () => provider.GetRequiredService<ICacheService>()
            );

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<Func<IEmailService>>(provider =>
            () => provider.GetRequiredService<IEmailService>()
            );

            services.AddHttpClient<IRegistrationClientService, RegistrationClientService>();
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            return services;
        }
    }
}
