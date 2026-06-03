using Shared.Dtos.ReportsAndDashboard.ConferencesAndSeminarsModule;
using Shared.Dtos.ReportsAndDashboard.FacultyMemberDataModule;
using Shared.Dtos.ReportsAndDashboard.ResearchesModule;
using Shared.Dtos.ReportsAndDashboard.WrtingsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ConferencesAndSeminarsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.FacultyMembersDataModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ResearchesModule;
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
    }
}
