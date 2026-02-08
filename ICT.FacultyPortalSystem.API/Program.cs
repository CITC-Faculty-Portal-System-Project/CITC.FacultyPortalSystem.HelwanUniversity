using ICIT.FacultyPortalSystem.API.Extensions;

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

            #endregion

            #region Pipelines - Middlewares

            #endregion
            
            var app = builder.Build();

            //app.UseExceptionHandlingMiddlewares();

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
