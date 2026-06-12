using Shared.Enums.AcademicDataModule.MissionsModule;
using Shared.Enums.ReportsModule;

namespace Shared.SpecificationParameters.ReportsAndDashboard.Base.ConferencesAndSeminarsModule
{
    public class BaseConferencesAndSeminarsReportSpecifiactionParamters
    {
        public List<int>? FacultyIds { get; set; }
        public List<int>? DepartmentIds { get; set; }
        public ConferenceOrSeminar? Type { get; set; }
        public ConferencesAndSeminarsSortingOptions? Sort { get; set; }
    }
}
