using Shared.ReportsAndDashboard;

namespace Services.Abstraction.Contracts.ReportsAndDashboard
{
    public interface IDashboardService
    {
        public Task<AdminDashboardResponseDTO> GetAdminDashboardDataAsync();
    }
}
