using Shared.Enums.ReportsModule;

namespace Shared.SpecificationParameters.ReportsAndDashboard.Base.ProjectsAndComiteesModule
{
    public class BaseParticipationInMagazineReportSpecificationParameters
    {
        public List<int>? FacultyIds { get; set; }
        public List<int>? DepartmentIds { get; set; }
        public List<Guid>? TypesOfParticipation { get; set; }
        public ParticipationInMagazineReportSortingOptions? Sort { get; set; }
    }
}
