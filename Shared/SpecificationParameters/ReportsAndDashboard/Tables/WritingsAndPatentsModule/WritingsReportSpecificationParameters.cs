using Shared.SpecificationParameters.ReportsAndDashboard.Base.WritingsModule;

namespace Shared.SpecificationParameters.ReportsAndDashboard.Tables.WritingsModule
{
    public class WritingsReportSpecificationParameters : BaseWritingsReportSpecificationParameters
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
