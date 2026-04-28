namespace Shared.Dtos.ReportsAndDashboard
{
    public record ResearchersInterestsStats
    {
        public int TotalInterestsNo { get; set; }
        public IReadOnlyList<InterestDetailedStats> DetailedStats { get; set; } = new List<InterestDetailedStats>();    
    }
}
