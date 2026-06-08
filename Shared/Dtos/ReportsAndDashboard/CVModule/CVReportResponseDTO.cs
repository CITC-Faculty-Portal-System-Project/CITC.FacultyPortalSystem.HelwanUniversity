namespace Shared.Dtos.ReportsAndDashboard.CVModule
{
    public record CVReportResponseDTO
    {
        public string FacultyName { get; set; } = string.Empty;
        public int NoOfCVs { get; set; }
        public List<DepartmentCVReportResponseDTO> DepartmentCVs { get; set; } = new List<DepartmentCVReportResponseDTO>();
    }
}
