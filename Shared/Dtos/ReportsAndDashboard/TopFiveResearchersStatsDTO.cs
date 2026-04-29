namespace Shared.Dtos.ReportsAndDashboard
{
    public record TopFiveResearchersStatsDTO
    {
        public string ResearcherName { get; set; } = string.Empty;
        public int TotalResearchesNo { get; set; }
        public double Score { get; set; }
    }
}
