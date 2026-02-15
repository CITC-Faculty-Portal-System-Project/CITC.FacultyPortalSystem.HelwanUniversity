using Services.Abstraction.Contracts.Common;

namespace Presentation.Global
{
    public sealed class LangContext(IHttpContextAccessor _http) : ILangContext
    {
        public string Lang
        {
            get
            {
                var lang = _http.HttpContext?.Request.Headers["X-Lang"].ToString();
                return string.IsNullOrWhiteSpace(lang) ? "en" : lang.ToLower();
            }
        }

        public bool IsAr => Lang.StartsWith("ar");
    }

}
