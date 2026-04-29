using Domain.Entities.AcademicDataModule.ResearchesModule;
using Shared.Dtos.ReportsAndDashboard;
using Shared.SpecificationParameters.ReportsAndDashboard;

namespace Services.Specifications.AggregationSpecifications
{
    internal class ResearchesPerDepartmentSpecification
        : AggregationSpecification<Research, ResearchDepartmentStatsDTO>
    {
        private readonly ResearchesPerDepartmentSpecificationParameters _parameters;

        public ResearchesPerDepartmentSpecification(
            ResearchesPerDepartmentSpecificationParameters parameters)
        {
            _parameters = parameters;

            SetCriteria(r => !r.IsDeleted && r.Contributions!.Any(c => c.IsConfirmed));
        }

        public override IQueryable<ResearchDepartmentStatsDTO> Apply(IQueryable<Research> query)
        {
            var filtered = query.Where(Criteria!);

            var departmentResearchesStats = filtered
                .SelectMany(r => r.Contributions!.Where(c => c.IsConfirmed)
                    .Select(c => new
                    {
                        Dept = c.Contributor!.PersonalData!.Department,
                        r.Id,
                        FacultyId = c.Contributor.PersonalData.Faculty.Id
                    }))
                .Where(x => x.FacultyId == _parameters.FacultyIdDepartmentResearches)
                .Distinct()
                .GroupBy(x => new { x.Dept.Id, x.Dept.NameAR, x.Dept.NameEN })
                .Select(g => new ResearchDepartmentStatsDTO
                {
                    DepartmentNameAR = g.Key.NameAR,
                    DepartmentNameEN = g.Key.NameEN,
                    ResearchesNo = g.Select(x => x.Id).Distinct().Count()
                });

            return departmentResearchesStats;
        }
    }
}