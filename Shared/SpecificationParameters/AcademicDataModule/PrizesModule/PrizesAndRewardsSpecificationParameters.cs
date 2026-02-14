using Shared.Enums.AcademicDataModule.ExperiencesModule;
using Shared.Enums.AcademicDataModule.PrizesModule;

namespace Shared.SpecificationParameters.AcademicDataModule.PrizesModule
{
    public class PrizesAndRewardsSpecificationParameters
    {
        private const int defaultPageSize = 9;
        private const int maxPageSize = 9;
        public string FacultyMemberEmail { get; set; } = string.Empty;
        public PrizesAndRewardsSortingOptions Sort { get; set; }
        public string? Search { get; set; }
        public List<Guid>? PrizeIds { get; set; }
        public int PageIndex { get; set; } = 1;
        private int _pageSize = defaultPageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }
    }
}