using Shared.Enums.ResearchesModule;

namespace Shared.Dtos.ReportsAndDashboard.ResearchesModule
{
    public record ResearchesPerYearReportResponseDTO
    {
        public string ResearchTitle { get; set; } = string.Empty;
        public PublicationType PublicationType { get; set; } = PublicationType.Local;
        public int? PubYear { get; set; }
    }
}
