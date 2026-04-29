using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Enums;
using Shared.Dtos.ReportsAndDashboard;
using Shared.SpecificationParameters.ReportsAndDashboard;

namespace Services.Specifications.AggregationSpecifications
{
    public class ResearchDashboardAggregationSpecification
        : AggregationSpecification<Research, ResearchesDashboardDTO>
    {

        public ResearchDashboardAggregationSpecification()
        {

            SetCriteria(r => !r.IsDeleted && r.Contributions!.Any(c => c.IsConfirmed));
        }

        public override IQueryable<ResearchesDashboardDTO> Apply(IQueryable<Research> query)
        {
            var filtered = query.Where(Criteria!);

            #region Publication Stats

            var publicationStats = new
            {
                Local = filtered.Count(r => r.PublicationType == PublicationType.Local),
                International = filtered.Count(r => r.PublicationType == PublicationType.International)
            };

            #endregion

            #region Researchers Base Query

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
                });

            #endregion

            #region Max Values (Normalization)

            var researchersList = researchersQuery.ToList();

            var maxH = researchersList.Any() ? researchersList.Max(x => x.HIndex) : 0;
            var maxP = researchersList.Any() ? researchersList.Max(x => x.TotalPapers) : 0;
            var maxC = researchersList.Any() ? researchersList.Max(x => x.TotalCitations) : 0;

            #endregion

            #region University Top 5

            var universityTop5 = researchersList
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

            #endregion
        
            #region Interests

            var interestsQuery = filtered
                .SelectMany(r => r.Contributions!)
                .SelectMany(c => c.Contributor!.Researcher!.ResearcherInterests!)
                .Where(i => !i.IsDeleted);

            var interestsStatsQuery = interestsQuery
                .GroupBy(i => i.Interest!.Name)
                .Select(g => new TopFiveResearchersIntersetsStats
                {
                    InterestName = g.Key,
                    ResearchersNumber = g.Select(x => x.ResearcherId).Distinct().Count()
                });

            var top5Interests = interestsStatsQuery
                .OrderByDescending(x => x.ResearchersNumber)
                .Take(5)
                .ToList();

            var totalInterests = interestsStatsQuery.Count();

            #endregion

            #region Citations

            var citationsDetails = filtered
                .SelectMany(r => r.Cites!)
                .GroupBy(c => c.Year)
                .Select(g => new DetailedCitesStatsDTO
                {
                    Year = g.Key,
                    TotalCites = g.Sum(x => x.NumberOfCites)
                })
                .OrderBy(x => x.Year)
                .ToList();

            var totalCitations = filtered
                .SelectMany(r => r.Cites!)
                .Sum(c => (int?)c.NumberOfCites) ?? 0;

            var citationsStats = new ResearchCitationsStatsDTO
            {
                TotalCitationsNo = totalCitations,
                DetailedCitesStats = citationsDetails
            };

            #endregion

            #region Final Result

            var result = new ResearchesDashboardDTO
            {
                LocalResearchesNo = publicationStats?.Local ?? 0,
                InternationalResearchesNo = publicationStats?.International ?? 0,

                TotalNumberOfInterests = totalInterests,

                UniversityTopFiveResearchers = universityTop5,

                TopFiveResearchersInterestsStats = top5Interests,

                CitationsStats = new List<ResearchCitationsStatsDTO> { citationsStats }
            };

            #endregion

            return new List<ResearchesDashboardDTO> { result }.AsQueryable();
        }
    }
}
