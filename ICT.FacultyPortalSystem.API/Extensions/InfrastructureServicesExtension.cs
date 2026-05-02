using Domain.Contracts;
using Domain.Entities.IdentityModule.Users;
using FtpFileStorage.Factories;
using FtpFileStorage.Implementation;
using Integrations.HttpClientFactory;
using Integrations.Services;
using Messaging.AsyncMessaging;
using Messaging.AsyncMessaging.Consumer;
using Messaging.AsyncMessaging.Publisher;
using Messaging.AsyncMessaging.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Presentation.Authorization;
using Presistence.Data;
using Presistence.Identity;
using Presistence.Repositories;
using QuestPDF.Infrastructure;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Services.Abstraction.Contracts.AttachmentsModule;
using Services.Abstraction.Contracts.IdentityModule;
using Services.Implementations.IdnetityModule;
using Shared.Common;
using StackExchange.Redis;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using UserRole = Domain.Entities.IdentityModule.Users.Role;
using Serilog;
using ICIT.FacultyPortalSystem.API.Logger;

namespace ICIT.FacultyPortalSystem.API.Extensions
{
    public static class InfrastructureServicesExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer
                    (configuration.GetConnectionString("DefaultConnection") , 
                        ac => ac.MigrationsAssembly(typeof(StoreDbContext).Assembly.FullName));
            });

            services.AddDbContext<IdentityStoreDbContext>(options =>
            {
                options.UseSqlServer
                    (configuration.GetConnectionString("IdentityConnection")
                    , ic => ic.MigrationsAssembly(typeof(IdentityStoreDbContext).Assembly.FullName));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
           

            services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!);
            });

            services.AddSingleton<INationalNumberPubClient, NationalNumberPubClient>();

			services.Configure<RabbitMQSettings>(
                configuration.GetSection("RabbitMQSettings"));

            services.Configure<RabbitMQConnectionSettings>(
                configuration.GetSection("RabbitMQConnectionSettings"));

            services.AddSingleton<IRabbitMQConnection, RabbitMQConnection>();

            services.AddHostedService<ExternalDataConsumerClient>();
            services.AddScoped<IFTPClientFactory, FTPClientFactory>();
            services.AddScoped<IFTPFileStorageService, FTPFileStorageService>();
			services.AddHostedService<ExternalDataConsumerClient>();
            services.AddHostedService<RedisSubscriberService>();

            services.AddHostedService<ResearchDataConsumerClient>();
            services.AddHttpClient("Generic", client =>
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            });

			services.AddHttpContextAccessor(); //To get HttpContext in Serilog Custom Log Formatter

            services.AddScoped<IGenericHTTPClient, GenericHttpClient>();
            services.AddScoped<IResearchesDOIandORCIDLoadService, ResearchesDOIandORCIDLoadService>();
            
            QuestPDF.Settings.License = LicenseType.Community;

            services.AddSingleton(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            services.AddIdentity<User, UserRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<IdentityStoreDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<ICacheRepository, CacheRepository>();

            services.ValidateJwt(configuration);
            return services;
        }
        public static IServiceCollection ValidateJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions?.Issuer,
                    ValidAudience = jwtOptions?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("jwtToken"))
                        {
                            context.Token = context.Request.Cookies["jwtToken"];
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddScoped<IPermissionService, PermissionService>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            //services.AddHostedService<IdentitySeedHostedService>();
            services.AddAuthorization();
            return services;
        }
    }
}
