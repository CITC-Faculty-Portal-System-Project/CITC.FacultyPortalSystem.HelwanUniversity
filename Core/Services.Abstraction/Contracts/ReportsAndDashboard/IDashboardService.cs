using Shared.Dtos.ReportsAndDashboard;
using Shared.SpecificationParameters.ReportsAndDashboard;

namespace Services.Abstraction.Contracts.ReportsAndDashboard
{
    public interface IDashboardService
    {
        public Task<AdminDashboardResponseDTO> GetAdminDashboardDataAsync();
        public Task<ResearchesDashboardDTO> GetResearchDashboardDataAsync(ResearchesDashboardSpecificationParameters parameters);
    }
}
