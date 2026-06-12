using Shared.Enums.ReportsModule;

namespace Shared.SpecificationParameters.ReportsAndDashboard.Base.ReviewingArticlesModule
{
    public class BaseReviewingArticlesReportSpecificationParameters
    {
        public ReviewingArticlesReportSortingOptions? Sort { get; set; }
        public List<int>? FacultyIds { get; set; }
        public List<int>? DepartmentIds { get; set; }
    }
}
