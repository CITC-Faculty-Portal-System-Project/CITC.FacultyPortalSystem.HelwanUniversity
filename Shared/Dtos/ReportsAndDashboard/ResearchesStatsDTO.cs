namespace Shared.Dtos.ReportsAndDashboard
{
    public record ResearchesStatsDTO
    {
        public int TotalResearchesNumber { get; set; }
        public int InternalResearches { get; set; }
        public int ExternalResearches { get; set; }
    }
}
