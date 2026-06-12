using Domain.Entities.AcademicDataModule.ResearchesModule;
using Shared.Dtos.ReportsAndDashboard;
using Shared.SpecificationParameters.ReportsAndDashboard;

namespace Services.Specifications.AggregationSpecifications
{
    public class ResearchersPerFacultyAggregationSpecification
        : AggregationSpecification<Research, TopFiveResearchersStatsDTO>
    {
        private readonly int _facultyId;

        public ResearchersPerFacultyAggregationSpecification(
            int facultyId)
        {
            _facultyId = facultyId;

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
                    NameAR = c.Contributor!.PersonalData!.NameAr,
                    NameEN = c.Contributor!.PersonalData!.NameEn,
                    JobTitleAR = c.Contributor!.PersonalData!.Title.ValueAr,
                    JobTitleEN = c.Contributor!.PersonalData!.Title.ValueEn,
                    FacultyId = c.Contributor!.PersonalData!.FacultyId,
                    FacultyAR = c.Contributor.PersonalData.Faculty.NameAR,
                    FacultyEN = c.Contributor.PersonalData.Faculty.NameEN,
                    DepartmentAR = c.Contributor.PersonalData.Department.NameAR,
                    DepartmentEN = c.Contributor.PersonalData.Department.NameEN,
                    HIndex = c.Contributor.Researcher!.Hindex
                })
                .Select(g => new
                {
                    g.Key.ContributorId,
                    g.Key.NameAR,
                    g.Key.NameEN,
                    g.Key.JobTitleAR,
                    g.Key.JobTitleEN,
                    g.Key.FacultyAR,
                    g.Key.FacultyEN,
                    g.Key.FacultyId,
                    g.Key.DepartmentAR,
                    g.Key.DepartmentEN,
                    HIndex = g.Key.HIndex,
                    TotalPapers = g.Count(),
                    TotalCitations = g
                        .SelectMany(x => x.Research!.Cites!)
                        .Sum(c => (int?)c.NumberOfCites) ?? 0
                })
                .Where(x => x.FacultyId == _facultyId);

            var researchersList = researchersQuery.ToList();

            var maxH = researchersList.Any() ? researchersList.Max(x => x.HIndex) : 0;
            var maxP = researchersList.Any() ? researchersList.Max(x => x.TotalPapers) : 0;
            var maxC = researchersList.Any() ? researchersList.Max(x => x.TotalCitations) : 0;

            var facultyTop5 = researchersList
                .Select(x => new TopFiveResearchersStatsDTO
                {
                    ResearcherNameAR = x.NameAR,
                    ResearcherNameEN = x.NameEN,
                    ResearcherJobTitleAR = x.JobTitleAR,
                    ResearcherJobTitleEN = x.JobTitleEN,
                    ResearcherFacultyAR = x.FacultyAR,
                    ResearcherFacultyEN = x.FacultyEN,
                    TotalResearchesNo = x.TotalPapers,
                    DepartmentAR = x.DepartmentAR,
                    DepartmentEN = x.DepartmentEN,
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