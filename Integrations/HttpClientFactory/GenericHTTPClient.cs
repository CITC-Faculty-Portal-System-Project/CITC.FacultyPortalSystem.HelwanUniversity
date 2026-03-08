using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Http;


namespace Integrations.HttpClientFactory
{
    public sealed class GenericHttpClient : IGenericHTTPClient
    {
        private readonly IHttpClientFactory _factory;
        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

        public GenericHttpClient(IHttpClientFactory factory)
        {
            _factory = factory;
        }


        #region Helpers


        private static string BuildUrl(string url, IDictionary<string, string?>? query)
        {
            if (query == null || !query.Any())
                return url;

            var sb = new StringBuilder(url);
            sb.Append(url.Contains('?') ? '&' : '?');

            foreach (var q in query.Where(q => q.Value != null))
            {
                sb.Append(Uri.EscapeDataString(q.Key));
                sb.Append('=');
                sb.Append(Uri.EscapeDataString(q.Value!));
                sb.Append('&');
            }

            return sb.ToString().TrimEnd('&');
        }


        private async Task<HttpResult<T>> SendAsync<T>(
        HttpMethod method,
        string url,
        object? body,
        IDictionary<string, string>? headers,
        IDictionary<string, string?>? query,
        CancellationToken ct)
        {
            var client = _factory.CreateClient("Generic");

            url = BuildUrl(url, query);

            using var request = new HttpRequestMessage(method, url);

            if (headers != null)
            {
                foreach (var h in headers)
                    request.Headers.TryAddWithoutValidation(h.Key, h.Value);
            }

            if (body != null && method != HttpMethod.Get)
            {
                var json = JsonSerializer.Serialize(body, JsonOptions);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            try
            {
                var response = await client.SendAsync(request, ct);
                var raw = await response.Content.ReadAsStringAsync(ct);

                if (!response.IsSuccessStatusCode)
                {
                    return new HttpResult<T>(
                        false,
                        response.StatusCode,
                        default,
                        raw,
                        raw);
                }

                if (typeof(T) == typeof(string))
                {
                    return new HttpResult<T>(
                        true,
                        response.StatusCode,
                        (T)(object)raw,
                        null,
                        raw);
                }

                var data = JsonSerializer.Deserialize<T>(raw, JsonOptions);

                return new HttpResult<T>(
                    true,
                    response.StatusCode,
                    data,
                    null,
                    raw);
            }
            catch (Exception ex)
            {
                return new HttpResult<T>(
                    false,
                    0,
                    default,
                    ex.Message,
                    null);
            }
        }


        #endregion


        public Task<HttpResult<T>> GetAsync<T>(string url,
            IDictionary<string, string>? headers = null,
            IDictionary<string, string?>? query = null,
            CancellationToken ct = default)
            => SendAsync<T>(HttpMethod.Get, url, null, headers, query, ct);

        public Task<HttpResult<T>> PostAsync<T>(string url,
            object? body = null,
            IDictionary<string, string>? headers = null,
            CancellationToken ct = default)
            => SendAsync<T>(HttpMethod.Post, url, body, headers, null, ct);

        public Task<HttpResult<T>> PutAsync<T>(string url,
            object? body = null,
            IDictionary<string, string>? headers = null,
            CancellationToken ct = default)
            => SendAsync<T>(HttpMethod.Put, url, body, headers, null, ct);

        public Task<HttpResult<T>> PatchAsync<T>(string url,
            object? body = null,
            IDictionary<string, string>? headers = null,
            CancellationToken ct = default)
            => SendAsync<T>(HttpMethod.Patch, url, body, headers, null, ct);

        public async Task<HttpResult> DeleteAsync(string url,
            IDictionary<string, string>? headers = null,
            CancellationToken ct = default)
        {
            var result = await SendAsync<object>(HttpMethod.Delete, url, null, headers, null, ct);
            return new HttpResult(result.IsSuccess, result.StatusCode, result.Error, result.RawResponse);
        }

    

     
    }
}
