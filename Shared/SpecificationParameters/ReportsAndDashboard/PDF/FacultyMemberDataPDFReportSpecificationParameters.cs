using Shared.Enums.ReportsModule;

namespace Shared.SpecificationParameters.ReportsAndDashboard.PDF
{
    public class FacultyMemberDataPDFReportSpecificationParameters
    {
        public List<int>? FacultyIds { get; set; }
        public List<int>? DepartmentIds { get; set; }
        public FacultyMembersReportSortingOptions Sorting { get; set; }

    }
}
