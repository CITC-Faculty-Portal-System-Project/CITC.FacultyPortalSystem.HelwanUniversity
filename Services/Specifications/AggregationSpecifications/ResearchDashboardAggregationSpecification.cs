using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Enums;
using Shared.Dtos.ReportsAndDashboard;
using Shared.SpecificationParameters.ReportsAndDashboard;

namespace Services.Specifications.AggregationSpecifications
{
    public class ResearchDashboardAggregationSpecification
        : AggregationSpecification<Research, ResearchesDashboardDTO>
    {
        private readonly ResearchesDashboardSpecificationParameters _parameters;

        public ResearchDashboardAggregationSpecification(
            ResearchesDashboardSpecificationParameters parameters)
        {
            _parameters = parameters;

            SetCriteria(r => !r.IsDeleted);
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

            #region Faculty Top 5

            var facultyTop5 = researchersList
                .Where(x => x.FacultyId == _parameters.FacultyIdTopFiveResearchers)
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

            #region Department Researches Stats

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
                })
                .ToList();

            #endregion

            #region Department Researchers Stats

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
                })
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

            #region Totals

            var totalDepartments = departmentResearchesStats.Count;

            #endregion

            #region Final Result

            var result = new ResearchesDashboardDTO
            {
                LocalResearchesNo = publicationStats?.Local ?? 0,
                InternationalResearchesNo = publicationStats?.International ?? 0,

                TotalNumberOfInterests = totalInterests,
                TotalDepartments = totalDepartments,

                UniversityTopFiveResearchers = universityTop5,
                FacultyTopFiveResearchers = facultyTop5,

                DepartmentResearchesStats = departmentResearchesStats,
                DepartmentResearchersStats = departmentResearchersStats,

                TopFiveResearchersInterestsStats = top5Interests,

                CitationsStats = new List<ResearchCitationsStatsDTO> { citationsStats }
            };

            #endregion

            return new List<ResearchesDashboardDTO> { result }.AsQueryable();
        }
    }
}
