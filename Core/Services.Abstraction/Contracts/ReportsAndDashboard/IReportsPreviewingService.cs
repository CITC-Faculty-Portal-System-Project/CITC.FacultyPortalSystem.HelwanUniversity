using Shared.SpecificationParameters.ReportsAndDashboard;

namespace Services.Abstraction.Contracts.ReportsAndDashboard
{
    public interface IReportsPreviewingService
    {
        public Task<string> PreviewGeneralSystemInfoReportAsync(string? notes);
        public Task<string> PreviewResearchesReportAsync(string? notes);
        public Task<string> PreviewFacultyResearchesAndResearchersReportAsync(int facultyId , string? notes);
       
    }
}
