using Domain.Entities.AcademicDataModule.ResearchesModule;
using Shared.Dtos.ReportsAndDashboard;
using Shared.SpecificationParameters.ReportsAndDashboard;

namespace Services.Specifications.AggregationSpecifications
{
    public class ResearchersPerFacultyAggregationSpecification
        : AggregationSpecification<Research, TopFiveResearchersStatsDTO>
    {
        private readonly ResearchersPerFacultySpecificationParameters _parameters;

        public ResearchersPerFacultyAggregationSpecification(
            ResearchersPerFacultySpecificationParameters parameters)
        {
            _parameters = parameters;

            SetCriteria(r => !r.IsDeleted && r.Contributions!.Any(c => c.IsConfirmed));
        }

        public override IQueryable<TopFiveResearchersStatsDTO> Apply(IQueryable<Research> query)
        {
            var filtered = query.Where(Criteria!);

            var researchersQuery = filtered
                .SelectMany(r => r.Contributions!.Where(c => c.IsConfirmed))
                .GroupBy(c => new
                {
                    c.ContributorId,
                    Name = c.Contributor!.PersonalData!.NameEn,
                    FacultyId = c.Contributor.PersonalData.Faculty.Id,
                    HIndex = c.Contributor.Researcher!.Hindex
                })
                .Select(g => new
                {
                    g.Key.ContributorId,
                    g.Key.Name,
                    g.Key.FacultyId,
                    HIndex = g.Key.HIndex,
                    TotalPapers = g.Count(),
                    TotalCitations = g
                        .SelectMany(x => x.Research!.Cites!)
                        .Sum(c => (int?)c.NumberOfCites) ?? 0
                })
                .Where(x => x.FacultyId == _parameters.FacultyIdTopFiveResearchers);

            var researchersList = researchersQuery.ToList();

            var maxH = researchersList.Any() ? researchersList.Max(x => x.HIndex) : 0;
            var maxP = researchersList.Any() ? researchersList.Max(x => x.TotalPapers) : 0;
            var maxC = researchersList.Any() ? researchersList.Max(x => x.TotalCitations) : 0;

            var facultyTop5 = researchersList
                .Select(x => new TopFiveResearchersStatsDTO
                {
                    ResearcherName = x.Name,
                    TotalResearchesNo = x.TotalPapers,
                    Score =
                        (0.5 * (maxH == 0 ? 0 : (double)x.HIndex / maxH)) +
                        (0.3 * (maxC == 0 ? 0 : (double)x.TotalCitations / maxC)) +
                        (0.2 * (maxP == 0 ? 0 : (double)x.TotalPapers / maxP))
                })
                .OrderByDescending(x => x.Score)
                .Take(5)
                .ToList();

            return facultyTop5.AsQueryable();
        }
    }
}