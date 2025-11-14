using ICIT.FacultyPortalSystem.API.Middlewares;

namespace ICIT.FacultyPortalSystem.API.Extensions
{
    public static class WebApplicationExtension
    {
        public static WebApplication UseExceptionHandlingMiddlewares(this WebApplication app)
        {
            //Middleware ==> Handle exception
            // Configure the HTTP request pipeline.
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            return app;
        }

        public static WebApplication UseSwaggerMiddlewares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
