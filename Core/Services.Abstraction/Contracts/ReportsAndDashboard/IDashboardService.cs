using Shared.Dtos.ReportsAndDashboard;
using Shared.SpecificationParameters.ReportsAndDashboard;

namespace Services.Abstraction.Contracts.ReportsAndDashboard
{
    public interface IDashboardService
    {
        public Task<AdminDashboardResponseDTO> GetAdminDashboardDataAsync();
        public Task<ResearchesDashboardDTO> GetResearchDashboardDataAsync();
        public Task<IReadOnlyList<TopFiveResearchersStatsDTO>> GetFacultyTopResearchersDashboardDataAsync(int facultyId);
        public Task<IReadOnlyList<DepartmentResearchersStatsDTO>> GetDepartmentResearchersDashboardDataAsync(int facultyId);
        public Task<IReadOnlyList<ResearchDepartmentStatsDTO>> GetDepartmentResearchesDashboardDataAsync(int facultyId);
    }
}
