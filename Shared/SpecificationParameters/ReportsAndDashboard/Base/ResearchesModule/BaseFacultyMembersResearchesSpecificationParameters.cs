using Shared.Enums.ReportsModule;

namespace Shared.SpecificationParameters.ReportsAndDashboard.Base.ResearchesModule
{
    public class BaseFacultyMembersResearchesSpecificationParameters
    {
        public List<int>? FacultyIds { get; set; }
        public List<int>? DepartmentIds { get; set; }
        public FacultyMembersResearchesSortingOptions? Sort { get; set; }
        public List<int>? PubYear { get; set; }
    }
}
