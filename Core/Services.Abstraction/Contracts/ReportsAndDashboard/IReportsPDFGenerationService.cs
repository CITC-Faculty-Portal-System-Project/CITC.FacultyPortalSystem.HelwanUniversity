using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ConferencesAndSeminarsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.FacultyMembersDataModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ResearchesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.WritingsModule;

namespace Services.Abstraction.Contracts.ReportsAndDashboard
{
    public interface IReportsPDFGenerationService
    {
        public Task<byte[]> GenerateAdminDashboardReportAsync(string? notes);
        public Task<byte[]> GenerateResearchDashboardReportAsync(string? notes);
        public Task<byte[]> GenerateFacultyResearchesReportAsync(int facultyId, string? notes);
        public Task<byte[]> GenerateFacultyMembersReportAsync(FacultyMembersDataReportSpecificatonParameters parameters, string? notes);
        public Task<byte[]> GenerateFacultyMembersResearchesReportAsync(FacultyMembersResearchesSpecificationParameters parameters, string? notes);
        public Task<byte[]> GenerateResearchesPerYearReportAsync(ResearchesPerYearReportSpecificationParameters parameters, string? notes);
        public Task<byte[]> GenerateConferencesAndSeminarsReportAsync(ConferencesAndSeminarsReportSpecificationParameters parameters, string? notes);
        public Task<byte[]> GenerateWritingsReportAsync(WritingsReportSpecificationParameters parameters, string? notes);
    }
}
