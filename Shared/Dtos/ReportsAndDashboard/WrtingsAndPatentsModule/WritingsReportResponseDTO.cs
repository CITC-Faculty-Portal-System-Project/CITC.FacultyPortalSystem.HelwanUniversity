using Shared.Dtos.ReportsAndDashboard.WrtingsAndPatentsModule;

namespace Shared.Dtos.ReportsAndDashboard.WrtingsModule
{
    public record WritingsReportResponseDTO
    {
        public string FacultyMemberName { get; set; } = string.Empty;
        public List<FacultyMemberWritingsAnalysisDTO> Writings { get; set; } = new List<FacultyMemberWritingsAnalysisDTO>();
    }
}
