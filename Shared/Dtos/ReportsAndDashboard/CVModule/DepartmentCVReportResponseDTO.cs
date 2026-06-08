namespace Shared.Dtos.ReportsAndDashboard.CVModule
{
    public record DepartmentCVReportResponseDTO
        {
        public string DepartmentName { get; set; } = string.Empty;
        public int NoOfCVs { get; set; }
    }
}
