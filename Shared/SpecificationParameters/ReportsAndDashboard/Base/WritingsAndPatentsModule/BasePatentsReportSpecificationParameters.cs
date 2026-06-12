using Shared.Enums.AcademicDataModule.MissionsModule;
using Shared.Enums.ReportsModule;

namespace Shared.SpecificationParameters.ReportsAndDashboard.Base.WritingsAndPatentsModule
{
    public class BasePatentsReportSpecificationParameters
    {
        public List<int>? FacultyIds { get; set; }
        public List<int>? DepartmentIds { get; set; }
        public LocalOrInternational? LocalOrInternational { get; set; }
        public PatentsReportSortingOptions? Sort { get; set; }
    }
}
