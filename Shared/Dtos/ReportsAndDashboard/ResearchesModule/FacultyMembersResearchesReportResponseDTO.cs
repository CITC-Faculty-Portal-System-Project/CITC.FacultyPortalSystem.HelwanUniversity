namespace Shared.Dtos.ReportsAndDashboard.ResearchesModule
{
    public record FacultyMembersResearchesReportResponseDTO
    {
        public string FacultyMemberName { get; set; } = string.Empty;
        public int NoOfInternationalResearches { get; set; }
        public int NoOfLocalResearches { get; set; }
    }
}
