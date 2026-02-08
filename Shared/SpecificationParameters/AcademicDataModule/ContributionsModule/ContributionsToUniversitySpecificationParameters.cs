using Shared.Enums.AcademicDataModule.ContributionsModule;

namespace Shared.SpecificationParameters.AcademicDataModule.ContributionsModule
{
    public class ContributionsToUniversitySpecificationParameters
    {
        private const int defaultPageSize = 9;
        private const int maxPageSize = 9;
        public string FacultyMemberEmail { get; set; } = string.Empty;
        public ContributionsToUniversitySortingOptions Sort { get; set; }
        public string? Search { get; set; }
        public List<Guid>? TypeOfContributionIds { get; set; }
        public int PageIndex { get; set; } = 1;
        private int _pageSize = defaultPageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }
    }
}
