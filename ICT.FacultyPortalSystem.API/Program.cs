using ICIT.FacultyPortalSystem.API.Extensions;
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
				var accessor = services.GetRequiredService<IHttpContextAccessor>();
				//var scopeFactory = services.GetRequiredService<IServiceScopeFactory>();

				loggerConfiguration
					.ReadFrom.Configuration(context.Configuration)
                    .WriteTo.Sink(new KafkaLogSink(accessor/*, scopeFactory*/));
			});
			#endregion

			#region Pipelines - Middlewares

			#endregion

			var app = builder.Build();

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

            app.Run();
        }
    }
}
