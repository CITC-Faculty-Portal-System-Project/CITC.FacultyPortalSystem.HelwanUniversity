namespace Shared.Dtos.ReportsAndDashboard.ProjectsAndComiteesModule
{
    public class ProjectsReportResponseDTO
    {
        public string FacultyMemberName { get; set; } = string.Empty;
        public List<FacultyMemberProjectAnalysisDTO> Projects { get; set; } = new List<FacultyMemberProjectAnalysisDTO>();
    }
}
