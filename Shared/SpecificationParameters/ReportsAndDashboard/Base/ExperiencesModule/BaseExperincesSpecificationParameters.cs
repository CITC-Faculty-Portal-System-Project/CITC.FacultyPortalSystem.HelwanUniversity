using Shared.Enums.ReportsModule;

namespace Shared.SpecificationParameters.ReportsAndDashboard.Base.ExperiencesModule
{
    public class BaseExperincesSpecificationParameters
    {
        public List<int>? FacultyIds { get; set; }
        public List<int>? DepartmentIds { get; set; }
        public ExpereinceSortingOptions Sorting { get; set; }

    }
}

