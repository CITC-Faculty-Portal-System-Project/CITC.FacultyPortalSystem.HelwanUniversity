namespace Shared.Dtos.ReportsAndDashboard
{
    public record ResearchDepartmentStatsDTO
    {
        public string DepartmentNameAR { get; set; } = string.Empty;
        public string DepartmentNameEN { get; set; } = string.Empty;
        public int ResearchesNo { get; set; }
    }
}
