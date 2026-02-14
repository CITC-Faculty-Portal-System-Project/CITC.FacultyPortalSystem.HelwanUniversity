using Shared.Enums.AcademicDataModule.MissionsModule;

namespace Shared.SpecificationParameters.AcademicDataModule.MissionsModule
{
    public class TrainingProgramsSpecificationParameters
    {
        private const int defaultPageSize = 9;
        private const int maxPageSize = 9;
        public string FacultyMemberEmail { get; set; } = string.Empty;
        public TrainingProgramsSortingOptions Sort { get; set; }
        public string? Search { get; set; }
        public List<TrainingProgramType>? TrainingProgramTypes { get; set; }
        public List<TrainingProgramParticipationType>? TrainingProgramParticipationTypes { get; set; }
        public int PageIndex { get; set; } = 1;
        private int _pageSize = defaultPageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }
    }
}
