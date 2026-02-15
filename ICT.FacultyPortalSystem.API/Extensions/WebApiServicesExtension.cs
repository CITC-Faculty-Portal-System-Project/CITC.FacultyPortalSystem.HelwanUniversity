using ICIT.FacultyPortalSystem.API.Factories;
using ICIT.FacultyPortalSystem.API.Localisation;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Presentation.Filters;
using Presentation.Global;
using Services.Abstraction.Contracts.Common;
using System.Globalization;

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

            services.AddHttpContextAccessor();
            services.AddScoped<ILangContext, LangContext>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new() { Title = "CITC Faculty Portal System", Version = "v1" });
                options.AddSecurityDefinition("X-Lang", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "X-Lang",
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Language header (ar / en)"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "X-Lang"
                            }
                        },
                        new List<string>()
                    }
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
