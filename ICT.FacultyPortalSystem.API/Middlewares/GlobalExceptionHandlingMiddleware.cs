using Domain.Models;
using Integrations.Exceptions;
using Microsoft.Extensions.Localization;
using Shared.Localisation;
using System.Text.Json;
using ValidationException = Domain.Exceptions.ValidationException;

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
                ErrorMessage = _localizer["errors.EndPoint.notFound", context.Request.Path].Value
            
            }.ToString();
            await context.Response.WriteAsync(response);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            if (ex is ValidationException validationEx)
            {
                await HandleValidationExceptionAsync(context, validationEx);
                return;
            }

            var response = new ErrorDetails()
            {
                ErrorMessage = ex switch
                {
                    LocalizedException lex => _localizer[lex.Key, lex.Args].Value,
                   
                    HttpRequestException httpEx when httpEx.StatusCode == System.Net.HttpStatusCode.NotFound
                     => _localizer["errors.NationalNumber.notFound"].Value,

                    HttpRequestException httpEx
                        => _localizer["errors.NationalNumber.notFound", (int?)httpEx.StatusCode ?? 0].Value,


                    _ => ex.Message
                }
            };

            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                UserAlreadyExistsException => StatusCodes.Status409Conflict,
                BadRequestException => StatusCodes.Status400BadRequest,
                (_) => StatusCodes.Status500InternalServerError
            };


            response.StatusCode = context.Response.StatusCode;
            await context.Response.WriteAsync(response.ToString());
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new ValidationErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "Validation Failed",
                Errors = ex.Errors
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
