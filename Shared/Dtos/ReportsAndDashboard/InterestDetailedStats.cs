namespace Shared.Dtos.ReportsAndDashboard
{
    public record InterestDetailedStats
    {
        public string InterestName { get; set; } = string.Empty;
        public int ResearchersNo { get; set; }
    }
}
