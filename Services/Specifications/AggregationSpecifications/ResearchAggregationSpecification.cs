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
                .Select(pd => pd.Faculty!)
                .Distinct();

            return faculties
                .GroupJoin(
                    validData,
                    f => f.Id,
                    pd => pd.FacultyId,
                    (f, pdGroup) => new ResearchesPerFacultyDTO
                    {
                        FacultyNameAR = f.NameAR,
                        FacultyNameEN = f.NameEN,
                        TotalNumberOfResearches = pdGroup
                            .Select(x => x.Id)
                            .Distinct()
                            .Count()
                    });
        }
    }
}