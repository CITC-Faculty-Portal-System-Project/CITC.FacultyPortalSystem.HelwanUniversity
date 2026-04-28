namespace Shared.Dtos.ReportsAndDashboard
{
    public record ResearchCitationsStatsDTO
    {
        public int TotalCitationsNo { get; set; }
        public IReadOnlyList<DetailedCitesStatsDTO> DetailedCitesStats { get; set; } = new List<DetailedCitesStatsDTO>();
    }
}
