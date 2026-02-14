using Shared.Enums.AcademicDataModule.MissionsModule;

namespace Shared.SpecificationParameters.AcademicDataModule.MissionsModule
{
    public class SeminarsAndConferncesSpecificationParameters
    {
        private const int defaultPageSize = 9;
        private const int maxPageSize = 9;
        public string FacultyMemberEmail { get; set; } = string.Empty;
        public SeminarsAndConferencesSortingOptions Sort { get; set; }
        public string? Search { get; set; }
        public List<LocalOrInternational>? LocalOrInternational { get; set; }
        public List<ConferenceOrSeminar>? ConferenceOrSeminar { get; set; }
        public List<Guid>? RoleOfParticipationIds { get; set; }
        public int PageIndex { get; set; } = 1;
        private int _pageSize = defaultPageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }
    }
}
