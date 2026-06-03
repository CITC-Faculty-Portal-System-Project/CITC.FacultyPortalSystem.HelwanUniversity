using Shared.Enums.AcademicDataModule.MissionsModule;

namespace Shared.Dtos.ReportsAndDashboard.ConferencesAndSeminarsModule
{
    public record ConferenceAndSeminarsReportResponseDTO
    {
        public string FacultyMemberName { get; set; } = string.Empty;
        public ConferenceOrSeminar Type { get; set; }
        public int NoOfConferencesOrSeminars { get; set; }
    }
}
