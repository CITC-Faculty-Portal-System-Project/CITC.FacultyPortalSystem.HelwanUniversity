using Shared.Enums.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule
{
    public class ProjectsSpecifcationsParameters
    {
        private const int defaultPageSize = 9;
        private const int maxPageSize = 9;
        public string FacultyMemberEmail { get; set; } = string.Empty;
        public ProjectsSortingOptions Sort { get; set; }
        public string? Search { get; set; }
        public List<LocalOrInternational>? LocalOrInternationals { get; set; }
        public List<Guid>? ParticipationRoleIds { get; set; }
        public List<Guid>? TypeOfProjectIds { get; set; }
        public int PageIndex { get; set; } = 1;
        private int _pageSize = defaultPageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }
    }
}
