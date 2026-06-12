using Shared.Enums.ReportsModule;

namespace Shared.SpecificationParameters.ReportsAndDashboard.Base.ProjectsAndComiteesModule
{
    public class BaseProjectsReportSpecificationParameters
    {
        public List<int>? FacultyIds { get; set; }
        public List<int>? DepartmentIds { get; set; }
        public List<Guid>? TypesOfProject { get; set; }
        public ProjectsReportSortingOptions? Sort { get; set; }
    }
}
