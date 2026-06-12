using Services.Abstraction.Contracts.ReportsAndDashboard;
using Shared.Dtos.ReportsAndDashboard;

namespace Services.ReportsAndDashboard.Helpers
{
    public static class GetRequiredDataForDashboardReportsHelpers
    {

        public static async Task<GeneralSystemReportDTO> PrepareGeneralSystemReportDataAsync(IDashboardService dashboardService)
        {
            var stats = await dashboardService.GetAdminDashboardDataAsync();

            return new GeneralSystemReportDTO
            {
                Stats = stats,

                ScientificAnalysis =
                    ReportsServiceHelpers.GenerateScientificRecommendations(stats),

                OperationalAnalysis =
                    ReportsServiceHelpers.GenerateOperationalNotes(stats),

                UsersPerFacultyRows =
                    ReportsServiceHelpers.GenerateTableRows(
                        stats.UsersPerFaculty,
                        faculty => $@"
                <tr>
                    <td>{faculty.FacultyNameAR}</td>
                    <td>{faculty.TotalNumberOfUsers}</td>
                </tr>"),

                ResearchesPerFacultyRows =
                    ReportsServiceHelpers.GenerateTableRows(
                        stats.ResearchesPerFaculty,
                        faculty => $@"
                <tr>
                    <td>{faculty.FacultyNameAR}</td>
                    <td>{faculty.TotalNumberOfResearches}</td>
                </tr>")
            };
        }

        public static async Task<ResearchDashboardReportDTO> PrepareResearchDashboardReportDataAsync(IDashboardService dashboardService)
        {
            var stats = await dashboardService.GetResearchDashboardDataAsync();

            return new ResearchDashboardReportDTO
            {
                Stats = stats,

                SystemAnalysis =
                    ReportsServiceHelpers.GenerateDashboardAnalysis(stats),

                BestResearchersRows =
                    ReportsServiceHelpers.GenerateTableRows(
                        stats.UniversityTopFiveResearchers,
                        researcher => $@"
                <tr>
                    <td>{researcher.ResearcherNameAR}</td>
                    <td>{researcher.ResearcherFacultyAR}</td>
                    <td>{researcher.TotalResearchesNo}</td>
                    <td>{researcher.Score:F2}</td>
                </tr>"),

                InterestsRows =
                    ReportsServiceHelpers.GenerateTableRows(
                        stats.TopFiveResearchersInterestsStats,
                        interest => $@"
                <tr>
                    <td>{interest.InterestName}</td>
                    <td>{interest.ResearchersNumber}</td>
                </tr>"),

                CitationsRows =
                    ReportsServiceHelpers.GenerateTableRows(
                        stats.CitationsStats
                            .FirstOrDefault()?
                            .DetailedCitesStats
                            ?? new List<DetailedCitesStatsDTO>(),

                        cite => $@"
                <tr>
                    <td>{cite.Year}</td>
                    <td>{cite.TotalCites}</td>
                </tr>")
            };
        }

        public static async Task<FacultyResearchReportDTO> PrepareFacultyResearchReportDataAsync(int facultyId, IDashboardService dashboardService)
        {
            var facultyTopResearchers =
                await dashboardService
                    .GetFacultyTopResearchersDashboardDataAsync(facultyId);

            var departmentResearchers =
                await dashboardService
                    .GetDepartmentResearchersDashboardDataAsync(facultyId);

            var departmentResearches =
                await dashboardService
                    .GetDepartmentResearchesDashboardDataAsync(facultyId);

            var totalResearches =
                departmentResearches.Sum(x => x.ResearchesNo);

            var topDepartment =
                departmentResearches
                    .OrderByDescending(x => x.ResearchesNo)
                    .FirstOrDefault();

            var topDeptPercentage =
                totalResearches == 0 || topDepartment is null
                    ? 0
                    : (topDepartment.ResearchesNo * 100.0 /
                       totalResearches);

            var insights = new List<string>();

            if (topDepartment is not null)
            {
                insights.Add(
                    $@"يتصدر قسم
            {topDepartment.DepartmentNameAR}
            الإنتاج البحثي بنسبة
            {topDeptPercentage:F1}%
            من إجمالي أبحاث الكلية");
            }


            var departmentsData = departmentResearchers.Join(
                departmentResearches,
                researcher => researcher.DepartmentNameAR,
                research => research.DepartmentNameAR,
                (researcher, research) => new
                {
                    DepartmentName = research.DepartmentNameAR,
                    ResearchersNo = researcher.ResearchesNo,
                    ResearchesNo = research.ResearchesNo
                });

            var departmentsTableRows =
                ReportsServiceHelpers.GenerateTableRows(
                    departmentsData,
                    dept => $@"
            <tr>
                <td>{dept.DepartmentName}</td>
                <td>{dept.ResearchersNo}</td>
                <td>{dept.ResearchesNo}</td>
            </tr>");

            var counter = 0;

            var topResearchersRows =
                ReportsServiceHelpers.GenerateTableRows(
                    facultyTopResearchers,
                    researcher =>
                    {
                        counter++;

                        return $@"
                <tr>
                    <td style=""font-weight:bold;color:var(--gold);"">
                        #{counter}
                    </td>
                    <td>{researcher.ResearcherNameAR}</td>
                    <td>{researcher.DepartmentAR}</td>
                    <td>{researcher.TotalResearchesNo}</td>
                </tr>";
                    });

            return new FacultyResearchReportDTO
            {
                TopResearchers = facultyTopResearchers,

                DepartmentResearchers = departmentResearchers,

                DepartmentResearches = departmentResearches,

                DepartmentsTableRows = departmentsTableRows,

                TopResearchersRows = topResearchersRows,
                InsightsHtml = insights.FirstOrDefault()?? "لا يوجد"
            };
        }
    }
}