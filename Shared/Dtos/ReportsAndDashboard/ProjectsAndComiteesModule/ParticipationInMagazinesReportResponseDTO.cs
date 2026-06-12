namespace Shared.Dtos.ReportsAndDashboard.ProjectsAndComiteesModule
{
    public record ParticipationInMagazinesReportResponseDTO
    {
        public string FacultyMemberName { get; set; } = string.Empty;
        
        public List<FacultyMemberParticipationInMagazinesReportAnalysisDTO> Participations { get; set; } = new List<FacultyMemberParticipationInMagazinesReportAnalysisDTO>();
    }
}
