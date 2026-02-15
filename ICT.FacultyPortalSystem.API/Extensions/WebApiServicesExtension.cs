using ICIT.FacultyPortalSystem.API.Factories;
using Microsoft.AspNetCore.Mvc;
using Presentation.Filters;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using ICIT.FacultyPortalSystem.API.Localisation;

namespace ICIT.FacultyPortalSystem.API.Extensions
{
    public static class WebApiServicesExtension
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddLocalization();

            services.AddControllers()
                  .AddJsonOptions(options =>
                   {
                       options.JsonSerializerOptions.Converters.Add(
                           new System.Text.Json.Serialization.JsonStringEnumConverter()
                       );
                   })
                    .AddDataAnnotationsLocalization()
                    .AddViewLocalization(); 

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyMethod()
                           .AllowAnyHeader()
                           .WithOrigins("http://localhost:3000")
                           .AllowCredentials();
                });
            });


            var supportedCultures = new[] {
                new CultureInfo("en-US"),
                new CultureInfo("ar-EG"),
                new CultureInfo("fr-FR")
            };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("ar-EG");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Insert(0, new CustomHeaderRequestCultureProvider("X-Lang"));


                options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
            });

            services.AddEndpointsApiExplorer();
            services.AddScoped<BlockMaliciousExtensionsFilter>();

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
                
                options.UseInlineDefinitionsForEnums();  // <-- THIS FIXES QUERY PARAM ENUMS


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
