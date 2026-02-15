using Microsoft.AspNetCore.Localization;

namespace ICIT.FacultyPortalSystem.API.Localisation
{
    public sealed class CustomHeaderRequestCultureProvider : RequestCultureProvider
    {
        private readonly string _headerName;

        public CustomHeaderRequestCultureProvider(string headerName)
            => _headerName = headerName;

        public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.TryGetValue(_headerName, out var values))
                return Task.FromResult<ProviderCultureResult?>(null);

            var culture = values.ToString();

            return Task.FromResult<ProviderCultureResult?>(
                new ProviderCultureResult(culture, culture));
        }
    }
}
