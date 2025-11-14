namespace ICIT.FacultyPortalSystem.API.Extensions
{
    public static class CoreServicesExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(cfg => { }, typeof(AssemblyReference).Assembly);
            //services.AddScoped(IServiceManager, ServiceManager);

            return services;
        }
    }
}
