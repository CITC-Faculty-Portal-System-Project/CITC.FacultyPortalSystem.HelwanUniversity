using Shared.Enums.ReportsModule;
using Shared.Enums.ResearchesModule;

namespace Shared.SpecificationParameters.ReportsAndDashboard.Tables.ResearchesModule
{
    public class ResearchesPerYearReportSpecificationParameters : BaseReportsSpecificationParameters
    {
        public ResearchesPerYearReportSortingOptions Sort { get; set; }
        public PublicationType? PublicationType { get; set; }
        public List<int>? PubYears { get; set; }
    }
}
