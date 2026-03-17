using Shared.Models.CVGenerationModule;
using System.Text.Json;

namespace Services.Implementations.CVGenerationModule
{
    public static class CVVisibilityHelper
    {
        public static CVVisibilityConfig Deserialize(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return new CVVisibilityConfig();

            return JsonSerializer.Deserialize<CVVisibilityConfig>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
         ) ?? new CVVisibilityConfig();
        }

        public static string Serialize(CVVisibilityConfig config)
        {
            return JsonSerializer.Serialize(config, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            });
        }
    }
}
