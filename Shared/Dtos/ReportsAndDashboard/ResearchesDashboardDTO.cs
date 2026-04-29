namespace Shared.Dtos.ReportsAndDashboard
{
    public record ResearchesDashboardDTO
    {
        public int InternationalResearchesNo { get; set; }
        public int LocalResearchesNo { get; set; }
        public int TotalNumberOfInterests { get; set; }
        public int TotalDepartments { get; set; }
        public IReadOnlyList<TopFiveResearchersStatsDTO> UniversityTopFiveResearchers { get; set; } = new List<TopFiveResearchersStatsDTO>();
        public IReadOnlyList<TopFiveResearchersStatsDTO> FacultyTopFiveResearchers { get; set; } = new List<TopFiveResearchersStatsDTO>();
        public IReadOnlyList<ResearchDepartmentStatsDTO> DepartmentResearchesStats { get; set; } = new List<ResearchDepartmentStatsDTO>();
        public IReadOnlyList<DepartmentResearchersStatsDTO> DepartmentResearchersStats { get; set; } = new List<DepartmentResearchersStatsDTO>();
        public IReadOnlyList<TopFiveResearchersIntersetsStats> TopFiveResearchersInterestsStats { get; set; } = new List<TopFiveResearchersIntersetsStats>();
        public IReadOnlyList<ResearchCitationsStatsDTO> CitationsStats { get; set; } = new List<ResearchCitationsStatsDTO>();
    }
}
