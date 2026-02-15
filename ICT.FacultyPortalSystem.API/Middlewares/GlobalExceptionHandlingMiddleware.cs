using Integrations.Exceptions;
using Microsoft.Extensions.Localization;
using Shared.Localisation;

namespace ICIT.FacultyPortalSystem.API.Middlewares
{
    public class GlobalExceptionHandlingMiddleware(
        RequestDelegate _next, 
        ILogger<GlobalExceptionHandlingMiddleware> _logger ,
       IStringLocalizerFactory factory)
    {

        private readonly IStringLocalizer _localizer =
        factory.Create(
           baseName: "Shared.Localisation.Resources.Messages",
           location: "Shared" 
       );

        public async Task InvokeAsync(HttpContext context)
    {
            try
            {
                await _next(context);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                    await HandleNotFoundApiAsync(context);
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong ==> : {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleNotFoundApiAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = $"The endpoint with url {context.Request.Path} not found."
            }.ToString();
            await context.Response.WriteAsync(response);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                //ErrorMessage = ex.Message
            };

            var asm = typeof(Messages).Assembly;
            var names = asm.GetManifestResourceNames();

            foreach (var n in names)
            {
                Console.WriteLine(n);
            }


            response.ErrorMessage = ex switch
            {
                LocalizedException lex => _localizer[lex.Key, lex.Args].Value,
                HttpRequestException httpEx when httpEx.StatusCode == System.Net.HttpStatusCode.NotFound
                 => _localizer["errors.NationalNumber.notFound"].Value,

                HttpRequestException httpEx
                    => _localizer["errors.NationalNumber.notFound", (int?)httpEx.StatusCode ?? 0].Value,


                _ => ex.Message 
            };


            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                AttachmentAlreadyExist => StatusCodes.Status409Conflict,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                UserAlreadyExistsException => StatusCodes.Status409Conflict,
                BadRequestException => StatusCodes.Status400BadRequest,
                ValidationException validationException => HandleValidationException(validationException, response),
                NotFoundL => StatusCodes.Status404NotFound,
                (_) => StatusCodes.Status500InternalServerError
            };
            response.StatusCode = context.Response.StatusCode;
            await context.Response.WriteAsync(response.ToString());
        }

        private int HandleValidationException(ValidationException validationException, ErrorDetails response)
        {
            response.Errors = validationException.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}
