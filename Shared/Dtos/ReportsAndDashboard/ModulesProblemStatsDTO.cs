namespace Shared.Dtos.ReportsAndDashboard
{
    public record ModulesProblemStatsDTO
    {
        public string ModuleName { get; set; } = string.Empty;
        public int NumberOfProblems { get; set; }
    }
}
