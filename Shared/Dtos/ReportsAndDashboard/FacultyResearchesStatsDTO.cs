namespace Shared.Dtos.ReportsAndDashboard
{
    public record FacultyResearchesStatsDTO
    {
        public string FacultyNameAR { get; set; } = string.Empty;
        public string FacultyNameEN { get; set; } = string.Empty;
        public int TotalNumberOfResearchers { get; set; }
    }
}
