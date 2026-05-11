namespace Shared.Dtos.ReportsAndDashboard
{
    public record FacultyReportInsightsDTO
    {
        public string TopDepartmentName { get; set; } = string.Empty;
        public double TopDepartmentPercentage { get; set; }

        public string TopGrowthDepartment { get; set; } = string.Empty;
        public double TopGrowthValue { get; set; }

        public List<string> AutoInsights { get; set; } = [];
    }
}
