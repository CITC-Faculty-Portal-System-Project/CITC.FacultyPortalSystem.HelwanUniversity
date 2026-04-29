namespace Shared.Dtos.ReportsAndDashboard
{
    public record TopFiveResearchersIntersetsStats
    {
        public string InterestName { get; set; } = string.Empty;
        public int ResearchersNumber { get; set; }
    }
}
