using Shared.Dtos.ReportsAndDashboard.FacultyMemberDataModule;
using Shared.Dtos.ReportsAndDashboard.ResearchesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.FacultyMembersDataModule;
using Shared.SpecificationParameters.ReportsAndDashboard.ResearchesModule;

namespace Services.Abstraction.Contracts.ReportsAndDashboard
{
    public interface IReportsDataService
    {
        public Task<PaginatedResult<FacultyMembersDataReportResponseDTO>> GetFacultyMembersDataReportAsync(FacultyMembersDataReportSpecificatonParameters parameters);
        public Task<PaginatedResult<FacultyMembersResearchesReportResponseDTO>> GetFacultyMembersResearchesReportAsync(FacultyMembersResearchesSpecificationParameters parameters);
        public Task<PaginatedResult<ResearchesPerYearReportResponseDTO>> GetResearchesPeryearReportAsync(ResearchesPerYearReportSpecificationParameters parameters);
    }
}
