using Shared.Dtos.ResearchesModule.ResearchesDOIandORCIDLoadService;

namespace Shared.Dtos.ResearchesModule
{
    public record DOIResponseDTO
    {
        public string? doi { get; init; }
        public string? doi_url { get; init; }
        public string? url { get; init; }
        public string? title { get; init; }
        public List<CrossrefAuthorDTO>? authors { get; init; }
        public string? journal { get; init; }
        public string? publisher { get; init; }
        public string? type { get; init; }
        public int? year { get; init; }
        public string? volume { get; init; }
        public string? issue { get; init; }
        public string? pages { get; init; }
        public int? references_count { get; init; }
        public int? is_referenced_by_count { get; init; }
        public string? RelatedResearchLink { get; set; }
        public string? Abstract { get; set; }
    }
}
