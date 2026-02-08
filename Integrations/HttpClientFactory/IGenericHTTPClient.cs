using System.Net;

namespace Integrations.HttpClientFactory
{
    public interface IGenericHTTPClient
    {
        Task<HttpResult<T>> GetAsync<T>(
        string url,
        IDictionary<string, string>? headers = null,
        IDictionary<string, string?>? query = null,
        CancellationToken ct = default);

        Task<HttpResult<T>> PostAsync<T>(
            string url,
            object? body = null,
            IDictionary<string, string>? headers = null,
            CancellationToken ct = default);

        Task<HttpResult<T>> PutAsync<T>(
            string url,
            object? body = null,
            IDictionary<string, string>? headers = null,
            CancellationToken ct = default);

        Task<HttpResult<T>> PatchAsync<T>(
            string url,
            object? body = null,
            IDictionary<string, string>? headers = null,
            CancellationToken ct = default);

        Task<HttpResult> DeleteAsync(
            string url,
            IDictionary<string, string>? headers = null,
            CancellationToken ct = default);
    }

    public record HttpResult(
        bool IsSuccess,
        HttpStatusCode StatusCode,
        string? Error,
        string? RawResponse);

    public record HttpResult<T>(
        bool IsSuccess,
        HttpStatusCode StatusCode,
        T? Data,
        string? Error,
        string? RawResponse);

}

