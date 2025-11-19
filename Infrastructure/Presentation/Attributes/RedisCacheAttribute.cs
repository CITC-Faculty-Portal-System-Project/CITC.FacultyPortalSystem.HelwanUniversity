using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Services.Abstraction.Contracts;
using System.Text;

namespace Presentation.Attributes
{
    internal class RedisCacheAttribute(int durationInSeconds = 120) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<RedisCacheAttribute>>();
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;

            string key = GenerateKey(context.HttpContext.Request);
            try
            {
                var cached = await cacheService.GetCachedValueAsync(key);
                if (cached != null)
                {
                    logger.LogDebug("Cache hit: {Key}", key);
                    context.Result = new ContentResult
                    {
                        Content = cached,
                        ContentType = "application/json",
                        StatusCode = StatusCodes.Status200OK
                    };
                    return;
                }
                logger.LogDebug("Cache miss: {Key}", key);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Cache read failed for key {Key}", key);
            }

            var resultContext = await next.Invoke();
            try
            {
                if (resultContext.Result is OkObjectResult okObjectResult)
                {
                    await cacheService.SetCachedValueAsync(key, okObjectResult.Value, TimeSpan.FromSeconds(durationInSeconds));
                    logger.LogDebug("Cached response for {Key}", key);
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Cache write failed for key {Key}", key);
            }
        }

        private string GenerateKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path); 
            foreach (var item in request.Query.OrderBy(x => x.Key))
            {
                key.Append($"{item.Key}-{item.Value}");
            }
            return key.ToString();
        }
    }
}
