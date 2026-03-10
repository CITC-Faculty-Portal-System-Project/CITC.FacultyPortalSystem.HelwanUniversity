using Shared.Enums.ResearchesModule;

namespace Shared.SpecificationParameters.ResearchesModule
{
    public class ThesesSpecificationParameters
    {
        private const int defaultPageSize = 9;
        private const int maxPageSize = 9;
        public Guid FacultyMemberId { get; set; }
        public ThesesSortingOptions Sort { get; set; }
        public List<Guid>? GradeIds { get; set; }
        public ThesisType? Type { get; set; }
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;
        private int _pageSize = defaultPageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }

    }
}
