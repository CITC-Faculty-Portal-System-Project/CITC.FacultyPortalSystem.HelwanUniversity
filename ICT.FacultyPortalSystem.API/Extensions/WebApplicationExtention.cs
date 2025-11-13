namespace ICIT.FacultyPortalSystem.API.Extensions
{
    public static class WebApplicationExtension
    {
        public static WebApplication UseSwaggerMiddlewares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
