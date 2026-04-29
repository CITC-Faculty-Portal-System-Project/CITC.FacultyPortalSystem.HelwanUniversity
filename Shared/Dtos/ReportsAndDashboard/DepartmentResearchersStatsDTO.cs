namespace Shared.Dtos.ReportsAndDashboard
{
    public record DepartmentResearchersStatsDTO
    {
        public string DepartmentNameAR { get; set; } = string.Empty;
        public string DepartmentNameEN { get; set; } = string.Empty;
        public int ResearchesNo { get; set; }

    }
}
