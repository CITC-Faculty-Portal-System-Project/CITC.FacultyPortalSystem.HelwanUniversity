using Shared.Dtos.ReportsAndDashboard;

namespace Services.Abstraction.Contracts.ReportsAndDashboard
{
    public interface IDashboardService
    {
        public Task<AdminDashboardResponseDTO> GetAdminDashboardDataAsync();
        public Task<ResearchesDashboardDTO> GetResearchDashboardDataAsync();
    }
}
