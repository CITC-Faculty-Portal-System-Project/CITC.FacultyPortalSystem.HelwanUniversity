using Shared.Enums.ReportsModule;
using Shared.Enums.ResearchesModule;

namespace Shared.SpecificationParameters.ReportsAndDashboard.Base.ResearchesModule
{
    public class BaseResearchesPerYearReportSpecificationParameters
    {
        public List<int>? FacultyIds { get; set; }
        public List<int>? DepartmentIds { get; set; }
        public PublicationType? PublicationType { get; set; }
        public List<int>? PubYears { get; set; }
        public ResearchesPerYearReportSortingOptions Sort { get; set; }

    }
}
