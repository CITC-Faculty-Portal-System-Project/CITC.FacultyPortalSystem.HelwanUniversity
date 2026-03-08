using System.Text.Json.Serialization;

namespace Shared.Dtos.ResearchesModule.ResearchesDOIandORCIDLoadService
{
    public record RelatedResearchFromDOIDTO
    {
        [JsonPropertyName("journal-title")]
        public string? JournalTitle { get; set; }

        public string? ArticleTitle { get; set; }
        public string? Unstructured { get; set; }
    }
}
