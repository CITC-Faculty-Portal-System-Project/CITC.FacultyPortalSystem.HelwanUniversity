using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.UniversityFacultiesAndDepartments;
using Shared.ReportsAndDashboard;

namespace Services.Specifications.AggregationSpecifications
{
    public class ResearchAggregationSpecification
        : AggregationSpecification<Research, ResearchesPerFacultyDTO>
    {
        private readonly IQueryable<Faculty> _faculties;

        public ResearchAggregationSpecification(IQueryable<Faculty> faculties)
        {
            _faculties = faculties;
            SetCriteria(r =>
                !r.IsDeleted &&
                r.Contributions!.Any(c => c.IsConfirmed));
        }

        public override IQueryable<ResearchesPerFacultyDTO> Apply(IQueryable<Research> query)
        {
            var validData = query
                .Where(Criteria!)
                .SelectMany(r => r.Contributions!)
                .Where(c => c.IsConfirmed)
                .Select(c => c.Contributor!.PersonalData!);

            var researchCounts = validData
                .GroupBy(pd => pd.FacultyId)
                .Select(g => new
                {
                    FacultyId = g.Key,
                    Count = g.Select(pd => pd.Id).Distinct().Count()
                })
                .ToList();

            var allFaculties = _faculties
                .Where(f => !f.IsDeleted)
                .Select(f => new { f.Id, f.NameAR, f.NameEN })
                .ToList();

            var result = allFaculties
                .GroupJoin(
                    researchCounts,
                    f => f.Id,
                    rc => rc.FacultyId,
                    (f, rc) => new ResearchesPerFacultyDTO
                    {
                        FacultyNameAR = f.NameAR,
                        FacultyNameEN = f.NameEN,
                        TotalNumberOfResearches = rc.FirstOrDefault()?.Count ?? 0
                    })
                .ToList();

            return result.AsQueryable();
        }
    }
}