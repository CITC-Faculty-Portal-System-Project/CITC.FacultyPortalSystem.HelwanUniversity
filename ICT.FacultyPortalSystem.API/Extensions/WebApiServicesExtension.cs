using ICIT.FacultyPortalSystem.API.Factories;
using Microsoft.AspNetCore.Mvc;

namespace ICIT.FacultyPortalSystem.API.Extensions
{
    public static class WebApiServicesExtension
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyMethod()
                           .AllowAnyHeader()
                           //.WithOrigins("frontUrl");
                           .AllowAnyOrigin();
                });
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });
            return services;
        }
    }
}
