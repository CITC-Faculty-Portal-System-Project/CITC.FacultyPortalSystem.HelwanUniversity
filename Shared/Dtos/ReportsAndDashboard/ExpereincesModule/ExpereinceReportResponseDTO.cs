namespace Shared.Dtos.ReportsAndDashboard.ExpereincesModule
{
    public class ExpereinceReportResponseDTO
    {
        public string FacultyMemberName { get; set; } = string.Empty;
        public List<FacultyMemberExperienceGroupingDTO> Experiences { get; set; } = new List<FacultyMemberExperienceGroupingDTO>();
    }
}
