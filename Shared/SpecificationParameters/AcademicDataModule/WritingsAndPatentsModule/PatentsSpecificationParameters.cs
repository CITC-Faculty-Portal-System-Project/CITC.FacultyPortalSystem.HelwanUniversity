using Shared.Enums.AcademicDataModule.WritingsAndPatentsModule;

namespace Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule
{
    public class PatentsSpecificationParameters
    {
        private const int defaultPageSize = 9;
        private const int maxPageSize = 9;
        public string FacultyMemberEmail { get; set; } = string.Empty;
        public PatentsSortingOptions Sort { get; set; }
        public string? Search { get; set; }
        public List<LocalOrInternational>? LocalOrInternational { get; set; }
        public int PageIndex { get; set; } = 1;
        private int _pageSize = defaultPageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }
    }
}
