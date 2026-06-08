using Shared.Enums.ReportsModule;

namespace Shared.SpecificationParameters.ReportsAndDashboard.Base.CVModule
{
    public class BaseCVReportSpecificationParameters
    {
            public List<int>? FacultyIds { get; set; }
            public CVReportSortingOptions? Sort { get; set; }
    }
}
