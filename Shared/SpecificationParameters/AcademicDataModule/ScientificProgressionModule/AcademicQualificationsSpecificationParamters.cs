using Shared.Enums.AcademicDataModule.ScientificProgressionModule;

namespace Shared.SpecificationParameters.AcademicDataModule.ScientificProgressionModule
{
    public class AcademicQualificationsSpecificationParamters
    {
        private const int defaultPageSize = 9;
        private const int maxPageSize = 9;
        public string FacultyMemberEmail { get; set; } = string.Empty;
        public AcademicQualificationsSortingOptions Sort { get; set; }
        public string? Search { get; set; }
        public List<Guid>? QualificationIds { get; set; }
        public List<Guid>? GradeIds { get; set; }
        public List<Guid>? DispatchIds { get; set; }      
        public int PageIndex { get; set; } = 1;
        private int _pageSize = defaultPageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }
    }
}
