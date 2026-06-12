namespace Shared.Dtos.ReportsAndDashboard
{
    public record GeneralSystemReportDTO
    {
        public AdminDashboardResponseDTO Stats { get; set; } = new();
        public string ScientificAnalysis { get; set; } = string.Empty;
        public string OperationalAnalysis { get; set; } = string.Empty;
        public string UsersPerFacultyRows { get; set; } = string.Empty;
        public string ResearchesPerFacultyRows { get; set; } = string.Empty;
    }
}
