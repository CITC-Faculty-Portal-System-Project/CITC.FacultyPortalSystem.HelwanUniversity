using Shared.SpecificationParameters.ReportsAndDashboard.PDF;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.ConferencesAndSeminarsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.FacultyMembersDataModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.ResearchesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.WritingsModule;
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
        public Task<byte[]> GenerateFacultyMembersReportAsync(FacultyMembersDataReportPdfSpecificationParameters parameters, string? notes);
        public Task<byte[]> GenerateFacultyMembersResearchesReportAsync(FacultyMembersResearchesPdfSpecificationParameters parameters, string? notes);
        public Task<byte[]> GenerateResearchesPerYearReportAsync(ResearchesPerYearPdfReportSpecificationParameters parameters, string? notes);
        public Task<byte[]> GenerateConferencesAndSeminarsReportAsync(ConferencesAndSeminarsReportPdfSpecificationParameters parameters, string? notes);
        public Task<byte[]> GenerateWritingsReportAsync(WritingsReportPdfSpecificationParameters parameters, string? notes);
    }
}
