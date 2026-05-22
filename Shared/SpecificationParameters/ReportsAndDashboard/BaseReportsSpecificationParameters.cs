using Shared.Dtos.ReportsAndDashboard;

namespace Shared.SpecificationParameters.ReportsAndDashboard
{
    public class BaseReportsSpecificationParameters
    {
        public List<int>? FacultyIds { get; set; }
        public List<int>? DepartmentIds { get; set; }
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
