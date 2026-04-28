using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Enums;
using Shared.Dtos.ReportsAndDashboard;

namespace Services.Specifications.AggregationSpecifications
{
    public class ResearchDashboardAggregationSpecification
        : AggregationSpecification<Research, ResearchesDashboardDTO>
    {
        public ResearchDashboardAggregationSpecification()
        {
            SetCriteria(r =>
                !r.IsDeleted);
        }

        public override IQueryable<ResearchesDashboardDTO> Apply(IQueryable<Research> query)
        {
            var filtered = query.Where(Criteria!);

            var publicationStats = filtered
                .GroupBy(r => 1)
                .Select(g => new
                {
                    Local = g.Count(r => r.PublicationType == PublicationType.Local),
                    International = g.Count(r => r.PublicationType == PublicationType.International)
                })
                .FirstOrDefault();

            var facultyStats = filtered
                .SelectMany(r => r.Contributions!.Where(c => c.IsConfirmed))
                .GroupBy(c => new
                {
                    c.Contributor!.PersonalData!.Faculty!.Id,
                    c.Contributor.PersonalData.Faculty.NameAR,
                    c.Contributor.PersonalData.Faculty.NameEN
                })
                .Select(g => new FacultyResearchesStatsDTO
                {
                    FacultyNameAR = g.Key.NameAR,
                    FacultyNameEN = g.Key.NameEN,
                    TotalNumberOfResearchers = g
                        .Select(x => x.ContributorId)
                        .Distinct()
                        .Count()
                })
                .ToList();

            var deptStats = filtered
                .SelectMany(r => r.Contributions!.Where(c => c.IsConfirmed)
                    .Select(c => new
                    {
                        Dept = c.Contributor!.PersonalData!.Department,
                        r.Id
                    }))
                .Distinct()
                .GroupBy(x => new { x.Dept.Id, x.Dept.NameAR, x.Dept.NameEN })
                .Select(g => new ResearchDepartmentStatsDTO
                {
                    DepartmentNameAR = g.Key.NameAR,
                    DepartmentNameEN = g.Key.NameEN,
                    ResearchesNo = g.Select(x => x.Id).Distinct().Count()
                })
                .ToList();

            var researchersStats = filtered
                .SelectMany(r => r.Contributions!)
                .GroupBy(c => new
                {
                    c.ContributorId,
                    Name = c.Contributor!.PersonalData!.NameEn
                })
                .Select(g => new ResearchersStatsDTO
                {
                    ResearcherName = g.Key.Name,
                    TotalResearchesNo = g.Count(),
                    ConfirmedResearchesNo = g.Count(x => x.IsConfirmed),
                    UnConfirmedResearchesNo = g.Count(x => !x.IsConfirmed)
                })
                .OrderByDescending(x => x.TotalResearchesNo)
                .Take(10)
                .ToList();

            var interests = filtered
                .SelectMany(r => r.Contributions!)
                .SelectMany(c => c.Contributor!.Researcher!.ResearcherInterests!)
                .Where(i => !i.IsDeleted)
                .GroupBy(i => i.Interest!.Name)
                .Select(g => new InterestDetailedStats
                {
                    InterestName = g.Key,
                    ResearchersNo = g.Select(x => x.ResearcherId).Distinct().Count()
                })
                .ToList();

            var interestsStats = new ResearchersInterestsStats
            {
                TotalInterestsNo = interests.Count,
                DetailedStats = interests
            };

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

            var result = new ResearchesDashboardDTO
            {
                LocalResearchesNo = publicationStats?.Local ?? 0,
                InternationalResearchesNo = publicationStats?.International ?? 0,
                FacultyStats = facultyStats,
                DepartmentStats = deptStats,
                ResearchersStats = researchersStats,
                InterestsStats = new List<ResearchersInterestsStats> { interestsStats },
                CitationsStats = new List<ResearchCitationsStatsDTO> { citationsStats }
            };

            return new List<ResearchesDashboardDTO> { result }.AsQueryable();
        }
    }
}