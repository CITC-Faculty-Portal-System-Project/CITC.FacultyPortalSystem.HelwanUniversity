namespace Shared.Dtos.ReportsAndDashboard
{
    public record ResearchesPerFacultyDTO
    {
        public string FacultyNameAR { get; set; } = string.Empty;
        public string FacultyNameEN { get; set; } = string.Empty;
        public int TotalNumberOfResearches { get; set; }

    }
}
