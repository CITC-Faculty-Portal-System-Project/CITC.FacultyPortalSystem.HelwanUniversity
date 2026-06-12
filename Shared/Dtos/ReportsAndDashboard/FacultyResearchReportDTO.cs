namespace Shared.Dtos.ReportsAndDashboard
{
    public record FacultyResearchReportDTO
    {
        public IReadOnlyList<TopFiveResearchersStatsDTO> TopResearchers
        { get; set; } = new List<TopFiveResearchersStatsDTO>();

        public IReadOnlyList<DepartmentResearchersStatsDTO>
            DepartmentResearchers
        { get; set; }
                = new List<DepartmentResearchersStatsDTO>();

        public IReadOnlyList<ResearchDepartmentStatsDTO>
            DepartmentResearches
        { get; set; }
                = new List<ResearchDepartmentStatsDTO>();

        public string InsightsHtml { get; set; } = string.Empty;

        public string DepartmentsTableRows { get; set; } = string.Empty;

        public string TopResearchersRows { get; set; } = string.Empty;
    }
}
