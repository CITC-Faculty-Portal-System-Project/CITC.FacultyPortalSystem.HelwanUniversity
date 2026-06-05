using Shared.Enums.ReportsModule;

namespace Shared.SpecificationParameters.ReportsAndDashboard.Base.FacultyMembersDataModule
{
    public class BaseFacultyMembersDataReportSpecificationParameters
    {
        public List<int>? FacultyIds { get; set; }
        public List<int>? DepartmentIds { get; set; }
        public FacultyMembersReportSortingOptions Sorting { get; set; }

    }
}
