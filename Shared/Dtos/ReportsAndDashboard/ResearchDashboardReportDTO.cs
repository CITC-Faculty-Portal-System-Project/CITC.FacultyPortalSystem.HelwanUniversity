namespace Shared.Dtos.ReportsAndDashboard
{
    public record ResearchDashboardReportDTO
    {
        public ResearchesDashboardDTO Stats { get; set; } = new();

        public string SystemAnalysis { get; set; } = string.Empty;

        public string BestResearchersRows { get; set; } = string.Empty;

        public string InterestsRows { get; set; } = string.Empty;

        public string CitationsRows { get; set; } = string.Empty;
    }
}
