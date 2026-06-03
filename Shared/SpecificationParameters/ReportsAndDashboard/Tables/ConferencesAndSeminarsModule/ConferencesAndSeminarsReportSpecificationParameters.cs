using Shared.Enums.AcademicDataModule.MissionsModule;
using Shared.Enums.ReportsModule;

namespace Shared.SpecificationParameters.ReportsAndDashboard.Tables.ConferencesAndSeminarsModule
{
    public class ConferencesAndSeminarsReportSpecificationParameters : BaseReportsSpecificationParameters
    {
        public ConferenceOrSeminar? Type { get; set; }
        public ConferencesAndSeminarsSortingOptions? Sort { get; set; }
    }
}
