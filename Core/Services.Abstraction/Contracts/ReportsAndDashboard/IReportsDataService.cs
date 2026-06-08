using Shared.Dtos.ReportsAndDashboard.ConferencesAndSeminarsModule;
using Shared.Dtos.ReportsAndDashboard.CVModule;
using Shared.Dtos.ReportsAndDashboard.ExpereincesModule;
using Shared.Dtos.ReportsAndDashboard.FacultyMemberDataModule;
using Shared.Dtos.ReportsAndDashboard.ProjectsAndComiteesModule;
using Shared.Dtos.ReportsAndDashboard.ResearchesModule;
using Shared.Dtos.ReportsAndDashboard.ReviewingArticlesModule;
using Shared.Dtos.ReportsAndDashboard.WrtingsAndPatentsModule;
using Shared.Dtos.ReportsAndDashboard.WrtingsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ConferencesAndSeminarsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.CVModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ExperincesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.FacultyMembersDataModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ProjectsAndComiteesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ResearchesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ReviewingArticlesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.WritingsAndPatentsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.WritingsModule;

namespace Services.Abstraction.Contracts.ReportsAndDashboard
{
    public interface IReportsDataService
    {
        public Task<PaginatedResult<FacultyMembersDataReportResponseDTO>> GetFacultyMembersDataReportAsync(FacultyMembersDataReportSpecificatonParameters parameters);
        public Task<PaginatedResult<FacultyMembersResearchesReportResponseDTO>> GetFacultyMembersResearchesReportAsync(FacultyMembersResearchesSpecificationParameters parameters);
        public Task<PaginatedResult<ResearchesPerYearReportResponseDTO>> GetResearchesPeryearReportAsync(ResearchesPerYearReportSpecificationParameters parameters);
        public Task<PaginatedResult<ConferenceAndSeminarsReportResponseDTO>> GetConferencesAndSeminarsReportAsync(ConferencesAndSeminarsReportSpecificationParameters parameters);
        public Task<PaginatedResult<WritingsReportResponseDTO>> GetWritingsReportAsync(WritingsReportSpecificationParameters parameters);
        public Task<PaginatedResult<CVReportResponseDTO>> GetCvsReportAsync(CVTableReportSpecificationParameters parameters);
        public Task<PaginatedResult<ExpereinceReportResponseDTO>> GetExperiencesReportAsync(ExperinceReportTableSpecificationParameters parameters);
        public Task<PaginatedResult<ReviewingArticlesReportResponseDTO>> GetReviewingArticlesReportAsync(ReviewingArticlesReportTableSpecificationParameters parameters);
        public Task<PaginatedResult<PatentsReportReponseDTO>> GetPatentsReportAsync(PatentsReportTableSpecificationParameters parameters);
        public Task<PaginatedResult<ProjectsReportResponseDTO>> GetProjectsReportAsync(ProjectsReportTableSpecificationParameters parameters);
        public Task<PaginatedResult<ParticipationInMagazinesReportResponseDTO>> GetParticipationInMagazinesReportAsync(ParticipationInMagazinesReportTableSpecificationParameters parameters);
    }
}
