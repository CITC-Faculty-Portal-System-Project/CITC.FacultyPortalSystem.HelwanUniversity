using Shared.Enums.AcademicDataModule.MissionsModule;

namespace Shared.Dtos.ReportsAndDashboard.ConferencesAndSeminarsModule
{
    public record FacultyMemberConferencesAndSeminarsAnalysisDTO
    {
        public ConferenceOrSeminar Type { get; set; }
        public int NoOfConferencesOrSeminars { get; set; }
    }
}
