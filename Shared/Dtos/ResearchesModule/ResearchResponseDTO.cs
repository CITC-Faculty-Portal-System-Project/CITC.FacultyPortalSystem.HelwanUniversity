using Shared.Dtos.AttachmentsModule;
using Shared.Enums.ResearchesModule;

namespace Shared.Dtos.ResearchesModule
{
    public record ResearchResponseDTO
    {
        public int Id { get; set; }
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
        public string PubYear { get; set; } = string.Empty;
        public ResearchSource Source { get; set; }
        public ResearchDerivedFrom ResearchDerivedFrom { get; set; }
        public string Abstract { get; set; } = string.Empty;
        public string? PubDate { get; set; } = string.Empty;
        public int? NoOfCititations { get; set; }
        public bool IsConfirmed { get; set; }
        public List<ResearchCitesResponseDTO>? Cites { get; set; }
        public List<ResearchContributionResponseDTO>? Contributions { get; set; }
        public List<AttachmentResponseDTO>? Attachments { get; set; }
    }
}
