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
            return query
                .Where(Criteria!)
                .SelectMany(r => r.Contributions!)
                .Where(c => c.IsConfirmed)
                .Select(c => c.Contributor!.PersonalData!)
                .GroupBy(pd => new
                {
                    pd.FacultyId,
                    pd.Faculty!.NameAR,
                    pd.Faculty!.NameEN
                })
                .Select(g => new ResearchesPerFacultyDTO
                {
                    FacultyNameAR = g.Key.NameAR,
                    FacultyNameEN = g.Key.NameEN,
                    TotalNumberOfResearches = g.Count()
                });
        }
    }
}