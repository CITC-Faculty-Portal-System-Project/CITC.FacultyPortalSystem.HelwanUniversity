namespace Shared.Dtos.ReportsAndDashboard
{
    public record ResearchesDashboardDTO
    {
        public int InternationalResearchesNo { get; set; }
        public int LocalResearchesNo { get; set; }
        public IReadOnlyList<FacultyResearchesStatsDTO> FacultyStats { get; set; } = new List<FacultyResearchesStatsDTO>();
        public IReadOnlyList<ResearchDepartmentStatsDTO> DepartmentStats { get; set; } = new List<ResearchDepartmentStatsDTO>();
        public IReadOnlyList<ResearchersStatsDTO> ResearchersStats { get; set; } = new List<ResearchersStatsDTO>();
    }
}
