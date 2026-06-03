using Shared.Enums.ReportsModule;

namespace Shared.SpecificationParameters.ReportsAndDashboard.Tables.ResearchesModule
{
    public class FacultyMembersResearchesSpecificationParameters : BaseReportsSpecificationParameters
    {
        public FacultyMembersResearchesSortingOptions? Sort { get; set; }
        public List<int>? PubYear { get; set; }
    }
}
