using Shared.Common;
using Shared.Enums.ResearchesModule;

namespace Shared.Dtos.ResearchesModule
{
    public record ResearchUpdateDTO
    {
        public string DOI { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? RelatedResearchLink { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string ResearchLink { get; set; } = string.Empty;
        public string JournalOrConfernce { get; set; } = string.Empty;
        public PublisherType PublisherType { get; set; }
        public PublicationType PublicationType { get; set; }
        public string? Issue { get; set; }
        public string? Volume { get; set; }
        public string? NoOfPages { get; set; }
        public int? PubYear { get; set; }
        public ResearchSource Source { get; set; }
        public ResearchDerivedFrom ResearchDerivedFrom { get; set; }
        public string Abstract { get; set; } = string.Empty;
        public string? PubDate { get; set; } = string.Empty;
        public List<ResearchContributionResponseDTO>? ResearchContributionsToDelete { get; set; }
        public List<ResearchContributionDTO>? ResearchContributionsToAdd { get; set; }
        public IEnumerable<Patch<int , ResearchContributionDTO>>? ResearchContributionsToUpdate { get; set; }

    }
}
