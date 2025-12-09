using System.Text.Json;

namespace Services.Helpers.ExternalDataFetchingServiceHelpers
{
    public static class JsonHelper
    {
        public static List<T> DeserializeListOrThrow<T>(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("JSON is null or empty.", nameof(json));

            return JsonSerializer.Deserialize<List<T>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<T>();
        }
    }
}
