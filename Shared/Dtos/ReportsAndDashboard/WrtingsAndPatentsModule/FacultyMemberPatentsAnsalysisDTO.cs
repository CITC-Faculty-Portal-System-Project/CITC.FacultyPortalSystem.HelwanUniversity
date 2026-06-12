using Shared.Enums.AcademicDataModule.MissionsModule;

namespace Shared.Dtos.ReportsAndDashboard.WrtingsAndPatentsModule
{
    public record FacultyMemberPatentsAnsalysisDTO
    {
        public LocalOrInternational Type { get; set; }
        public int NoOfPatents { get; set; }
    }
}
