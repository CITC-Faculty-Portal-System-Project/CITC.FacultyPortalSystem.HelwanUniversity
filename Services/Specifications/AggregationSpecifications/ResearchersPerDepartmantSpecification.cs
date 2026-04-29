using Domain.Entities.AcademicDataModule.ResearchesModule;
using Shared.Dtos.ReportsAndDashboard;
using Shared.SpecificationParameters.ReportsAndDashboard;

namespace Services.Specifications.AggregationSpecifications
{
    internal class ResearchersPerDepartmantSpecification
        : AggregationSpecification<Research, DepartmentResearchersStatsDTO>
    {
        private readonly ResearchersPerDepartmentSpecificationParameters _parameters;

        public ResearchersPerDepartmantSpecification(
            ResearchersPerDepartmentSpecificationParameters parameters)
        {
            _parameters = parameters;

            SetCriteria(r => !r.IsDeleted && r.Contributions!.Any(c=> c.IsConfirmed));
        }

        public override IQueryable<DepartmentResearchersStatsDTO> Apply(IQueryable<Research> query)
        {
            var filtered = query.Where(Criteria!);

            var departmentResearchersStats = filtered
                .SelectMany(r => r.Contributions!.Where(c => c.IsConfirmed))
                .Where(c => c.Contributor!.PersonalData!.Faculty.Id ==
                            _parameters.FacultyIdDepartmentResearchers)
                .GroupBy(c => new
                {
                    DeptId = c.Contributor!.PersonalData!.Department.Id,
                    DeptNameAR = c.Contributor.PersonalData.Department.NameAR,
                    DeptNameEN = c.Contributor.PersonalData.Department.NameEN
                })
                .Select(g => new DepartmentResearchersStatsDTO
                {
                    DepartmentNameAR = g.Key.DeptNameAR,
                    DepartmentNameEN = g.Key.DeptNameEN,
                    ResearchesNo = g.Select(x => x.ContributorId).Distinct().Count()
                });

            return departmentResearchersStats;
        }
    }
}