using Shared.SpecificationParameters.ReportsAndDashboard.Base.ReviewingArticlesModule;

namespace Shared.SpecificationParameters.ReportsAndDashboard.Tables.ReviewingArticlesModule
{
    public class ReviewingArticlesReportTableSpecificationParameters : BaseReviewingArticlesReportSpecificationParameters
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
