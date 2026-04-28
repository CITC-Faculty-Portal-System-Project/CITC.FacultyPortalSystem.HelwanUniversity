namespace Shared.Dtos.ReportsAndDashboard
{
    public record ResearchesMonthlyRateDTO
    {
        public string MonthAR { get; set; } = string.Empty;
        public string MonthEN { get; set; } = string.Empty;
        public int TotalNumberOfResearches { get; set; }

    }
}
