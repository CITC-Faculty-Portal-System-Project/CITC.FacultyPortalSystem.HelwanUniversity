using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.UniversityFacultiesAndDepartments;
using Shared.Dtos.ReportsAndDashboard;
using Shared.SpecificationParameters.ReportsAndDashboard;

namespace Services.Specifications.AggregationSpecifications
{
    internal class ResearchersPerDepartmentSpecification
     : AggregationSpecification<Department, DepartmentResearchersStatsDTO>
    {

        public ResearchersPerDepartmentSpecification(
            int facultyId)
        {

            SetCriteria(d => d.FacultyId == facultyId);
        }

        public override IQueryable<DepartmentResearchersStatsDTO> Apply(IQueryable<Department> query)
        {
            return query
                .Where(Criteria!)
                .Select(d => new DepartmentResearchersStatsDTO
                {
                    DepartmentNameAR = d.NameAR,
                    DepartmentNameEN = d.NameEN,
                    ResearchesNo = d.FacultyMembers!
                        .SelectMany(f => f.FacultyMember!.ResearchContributions!
                            .Where(c => c.IsConfirmed && !c.Research!.IsDeleted))
                        .Select(c => c.ContributorId)
                        .Distinct()
                        .Count()
                })
                .OrderByDescending(x => x.ResearchesNo);
        }
    }
}