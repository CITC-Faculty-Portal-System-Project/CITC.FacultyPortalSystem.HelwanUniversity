using Domain.Entities.IdentityModule.Users;
using ICIT.FacultyPortalSystem.API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence.Identity;
using Shared.Hubs;
using ICIT.FacultyPortalSystem.API.Logger;
using Microsoft.Extensions.Configuration;
using Serilog;
using Presistence.Data;
using Presistence.Identity.Seeding;

namespace ICIT.FacultyPortalSystem.API
{
    public class Program
    {
        public static async Task Main(string[] args)
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
			#endregion

			#region Pipelines - Middlewares

			#endregion

			var app = builder.Build();
            
            
            await app.UseIdentityDatabaseInitializerAsync();


            using (var scope = app.Services.CreateScope())
            {
                var systemdb = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
                systemdb.Database.Migrate();

                //    var identitydb = scope.ServiceProvider.GetRequiredService<IdentityStoreDbContext>();
                //    identitydb.Database.Migrate();

                await app.Services.SeedUserPermissionsAsync(
                Guid.Parse("A9923638-8866-4A89-A9FE-9CF329CFC8F7"),
                new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc));

                await app.Services.SeedRolePermissionsAsync();

            }

            app.UseExceptionHandlingMiddlewares();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Lax,
                Secure = CookieSecurePolicy.None
            });
            //     app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.MapHub<ChatHub>("/hubs/chatHub");

            app.Run();
        }
    }
}
