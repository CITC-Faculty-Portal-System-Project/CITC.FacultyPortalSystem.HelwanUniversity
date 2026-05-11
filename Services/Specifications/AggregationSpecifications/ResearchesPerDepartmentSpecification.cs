using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.UniversityFacultiesAndDepartments;
using Shared.Dtos.ReportsAndDashboard;
using Shared.SpecificationParameters.ReportsAndDashboard;

namespace Services.Specifications.AggregationSpecifications
{
    internal class ResearchesPerDepartmentSpecification
        : AggregationSpecification<Department, ResearchDepartmentStatsDTO>
    {
        public ResearchesPerDepartmentSpecification(
            int facultyId)
        {
            SetCriteria(d => d.FacultyId == facultyId);
        }

        public override IQueryable<ResearchDepartmentStatsDTO> Apply(IQueryable<Department> query)
        {
            return query
                .Where(Criteria!)
                .Select(d => new ResearchDepartmentStatsDTO
                {
                    DepartmentNameAR = d.NameAR,
                    DepartmentNameEN = d.NameEN,
                    ResearchesNo = d.FacultyMembers!
                        .SelectMany(f => f.FacultyMember!.ResearchContributions!
                            .Where(c => c.IsConfirmed && !c.Research!.IsDeleted))
                        .Select(c => c.ResearchId)
                        .Distinct()
                        .Count()
                })
                .OrderByDescending(x => x.ResearchesNo);
        }
    }
}