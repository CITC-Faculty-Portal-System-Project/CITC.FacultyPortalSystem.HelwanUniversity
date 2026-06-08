using Shared.SpecificationParameters.ReportsAndDashboard.PDF;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.ConferencesAndSeminarsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.CVModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.ExperienceModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.FacultyMembersDataModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.ProjectsAndComiteesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.ResearchesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.ReviewingArticlesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.WritingsAndPatentsModule;
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
        public Task<byte[]> GenerateCvReportAsync(CVPdfReportSpecificationParameters parameters, string? notes);
        public Task<byte[]> GenerateExperienceReportAsync(ExperienceReportPdfSpecificationParameters parameters, string? notes);
        public Task<byte[]> GenerateReviewingArticlesReportAsync(ReviewingArticlesReportPDFSpecificationParameters parameters, string? notes);
        public Task<byte[]> GeneratePatentsReportAsync(PatentsReportPdfSpecificationParameters parameters, string? notes);
        public Task<byte[]> GenerateProjectsReportAsync(ProjectsReportPdfSpecificationParameters parameters, string? notes);
        public Task<byte[]> GenerateParticipationInMagazinesReportAsync(ParticipationInMagazinesPdfReportSpecificationParamters parameters, string? notes);
    }
}
