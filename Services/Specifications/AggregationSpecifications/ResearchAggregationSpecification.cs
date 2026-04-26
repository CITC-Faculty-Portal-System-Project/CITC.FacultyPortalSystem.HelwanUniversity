using Domain.Entities.AcademicDataModule.ResearchesModule;
using Shared.ReportsAndDashboard;

namespace Services.Specifications.AggregationSpecifications
{
    public class ResearchAggregationSpecification
        : AggregationSpecification<Research, ResearchesPerFacultyDTO>
    {
        public ResearchAggregationSpecification()
        {
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

            var faculties = validData
                .Where(pd => pd.Faculty != null)
                .Select(pd => new { pd.Faculty!.Id, pd.Faculty.NameAR, pd.Faculty.NameEN })
                .Distinct()
                .ToList();

            var researchCounts = validData
                .GroupBy(pd => pd.FacultyId)
                .Select(g => new
                {
                    FacultyId = g.Key,
                    Count = g.Select(pd => pd.Id).Distinct().Count()
                })
                .ToList();

            // left join في الميموري عشان الكليات اللي ملهاش أبحاث ترجع بـ 0
            var result = faculties
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