namespace Shared.Dtos.ReportsAndDashboard.WrtingsAndPatentsModule
{
    public record PatentsReportReponseDTO
    {
        public string FacultyMemberName { get; set; } = string.Empty;
        public List<FacultyMemberPatentsAnsalysisDTO> Patents { get; set; } = new List<FacultyMemberPatentsAnsalysisDTO>();
    }
}
