using Shared.Enums.AcademicDataModule.MissionsModule;

namespace Shared.Dtos.ReportsAndDashboard.ConferencesAndSeminarsModule
{
    public record ConferenceAndSeminarsReportResponseDTO
    {
        public string FacultyMemberName { get; set; } = string.Empty;
        public List<FacultyMemberConferencesAndSeminarsAnalysisDTO> ConferencesAndSeminars { get; set; } = new List<FacultyMemberConferencesAndSeminarsAnalysisDTO>();
    }
}
