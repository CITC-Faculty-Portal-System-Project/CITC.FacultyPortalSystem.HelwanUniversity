using Shared.Dtos.ReportsAndDashboard;
using Shared.SpecificationParameters.ReportsAndDashboard;

namespace Services.Abstraction.Contracts.ReportsAndDashboard
{
    public interface IDashboardService
    {
        public Task<AdminDashboardResponseDTO> GetAdminDashboardDataAsync();
        public Task<ResearchesDashboardDTO> GetResearchDashboardDataAsync();
        public Task<IReadOnlyList<TopFiveResearchersStatsDTO>> GetFacultyTopResearchersDashboardDataAsync(ResearchersPerFacultySpecificationParameters parameters);
        public Task<IReadOnlyList<DepartmentResearchersStatsDTO>> GetDepartmentResearchersDashboardDataAsync(ResearchersPerDepartmentSpecificationParameters parameters );
        public Task<IReadOnlyList<ResearchDepartmentStatsDTO>> GetDepartmentResearchesDashboardDataAsync(ResearchesPerDepartmentSpecificationParameters parameters);
    }
}
