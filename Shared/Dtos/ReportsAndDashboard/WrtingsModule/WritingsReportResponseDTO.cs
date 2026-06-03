namespace Shared.Dtos.ReportsAndDashboard.WrtingsModule
{
    public record WritingsReportResponseDTO
    {
        public string FacultyMemberName { get; set; } = string.Empty;
        public string AuthorRole { get; set; } = string.Empty;
        public int NoOfWritings { get; set; }
    }
}
