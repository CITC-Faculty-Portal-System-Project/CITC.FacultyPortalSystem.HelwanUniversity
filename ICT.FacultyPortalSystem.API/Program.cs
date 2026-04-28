using Domain.Entities.IdentityModule.Users;
using ICIT.FacultyPortalSystem.API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence.Identity;
using Shared.Hubs;
using ICIT.FacultyPortalSystem.API.Logger;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace ICIT.FacultyPortalSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region DI Container
            var builder = WebApplication.CreateBuilder(args);

            //WebApi Services
            builder.Services.AddWebApiServices(builder.Configuration);

            //Infrastructure Services
            builder.Services.AddInfrastructureServices(builder.Configuration);

            //Core Services
            builder.Services.AddCoreServices(builder.Configuration);

			//Serilog Configuration
			builder.Host.UseSerilog((context, services, loggerConfiguration) =>
			{
                loggerConfiguration
                    .ReadFrom.Configuration(context.Configuration)
                    .WriteTo.Sink(new KafkaLogSink());
                    
			});

            builder.WebHost.UseSentry(options =>
            {
                options.Dsn = "https://bdb6297d52d2d6af53037ef6ca79734a@o4511298747629568.ingest.de.sentry.io/4511299390996560";

                options.Debug = true;
                options.TracesSampleRate = 1.0;
                options.MaxQueueItems = 100;
            });

            #endregion

            #region Pipelines - Middlewares

            #endregion

            var app = builder.Build();

            app.UseSentryTracing();

            app.UseExceptionHandlingMiddlewares();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.None
            });

            //     app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.MapHub<ChatHub>("/hubs/chatHub");

            app.MapHub<NotificationHub>("/hubs/notificationHub");

            app.Run();
        }
    }
}
