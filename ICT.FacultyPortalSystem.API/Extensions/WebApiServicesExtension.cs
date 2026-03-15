using ICIT.FacultyPortalSystem.API.Factories;
using Microsoft.AspNetCore.Mvc;
using Presentation.Filters;
using Services.EncryptionServices.Configurations;

namespace ICIT.FacultyPortalSystem.API.Extensions
{
    public static class WebApiServicesExtension
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
                  .AddJsonOptions(options =>
                   {
                       options.JsonSerializerOptions.Converters.Add(
                           new System.Text.Json.Serialization.JsonStringEnumConverter()
                       );
                   });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyMethod()
                           .AllowAnyHeader()
                           .WithOrigins(
                        "http://localhost:3000",
                        "http://localhost",
                        "http://127.0.0.1",
                        "http://localhost:80",
                        "http://127.0.0.1:80"
                        )
                         .AllowCredentials();
                });
            });

            services.AddEndpointsApiExplorer();
            services.AddScoped<BlockMaliciousExtensionsFilter>();
            services.AddSignalR();
            services.Configure<MessageEncryption>(
                configuration.GetSection("MessageEncryption"));


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new() { Title = "CITC Faculty Portal System", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Bearer <token>"
                });
                
                options.UseInlineDefinitionsForEnums();  


                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            }); services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });
            return services;
        }
    }
}
