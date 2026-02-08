using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Domain.Entities.AcademicDataModule.ResearchesModule
{
    public class Research : BaseEntity<int>
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
        public string PubYear { get; set; } = string.Empty;
        public ResearchSource Source { get; set; }
        public ResearchDerivedFrom ResearchDerivedFrom { get; set; }
        public string Abstract { get; set; } = string.Empty;
        public string? PubDate { get; set; } = string.Empty;
        public int? NoOfCititations { get; set; }
        public int? ThesisId { get; set; }
        public Thesis? Thesis { get; set; }
        public ICollection<ResearchContribution>? Contributions { get; set; } = new List<ResearchContribution>();
        public ICollection<ResearchCite>? Cites { get; set; } = new List<ResearchCite>();
        public ICollection<ResearchAttachment>? Attachments { get; set; } = new List<ResearchAttachment>();

    }
}
