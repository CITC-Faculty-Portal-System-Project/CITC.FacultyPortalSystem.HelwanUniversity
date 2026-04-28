namespace Shared.Dtos.ReportsAndDashboard
{
    public record ResearchersStatsDTO
    {
        public string ResearcherName { get; set; } = string.Empty;
        public int TotalResearchesNo { get; set; }
        public int ConfirmedResearchesNo { get; set; }
        public int UnConfirmedResearchesNo { get; set; }
    }
}
