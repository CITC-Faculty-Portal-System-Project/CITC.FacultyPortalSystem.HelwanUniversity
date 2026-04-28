using Microsoft.AspNetCore.Authorization;
using Shared.Dtos.ReportsAndDashboard;

namespace Presentation.Controllers.DashboardAndReportsModule
{
    public class DashboardAndReportsController(IServiceManager _serviceManager) : ApiController
    {
        [Authorize(Policy = "Permission:Reports.Read")]
        [ProducesResponseType(typeof(AdminDashboardResponseDTO), StatusCodes.Status200OK)]
            [HttpGet("Dashboard")]
            public async Task<ActionResult<AdminDashboardResponseDTO>> GetDashboardData()
                => Ok(await _serviceManager.DashboardService.GetAdminDashboardDataAsync());
    }
}
