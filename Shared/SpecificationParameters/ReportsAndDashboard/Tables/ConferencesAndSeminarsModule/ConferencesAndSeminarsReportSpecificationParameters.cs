using Shared.Enums.AcademicDataModule.MissionsModule;
using Shared.Enums.ReportsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Base.ConferencesAndSeminarsModule;

namespace Shared.SpecificationParameters.ReportsAndDashboard.Tables.ConferencesAndSeminarsModule
{
    public class ConferencesAndSeminarsReportSpecificationParameters : BaseConferencesAndSeminarsReportSpecifiactionParamters
    {
        public string? Search { get; set; }
        private const int defaultPageSize = 9;
        private const int maxPageSize = 9;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = defaultPageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }
    }
}
