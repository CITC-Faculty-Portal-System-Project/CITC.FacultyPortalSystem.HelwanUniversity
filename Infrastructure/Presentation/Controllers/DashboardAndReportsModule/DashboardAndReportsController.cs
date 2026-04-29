using Microsoft.AspNetCore.Authorization;
using Shared.Dtos.ReportsAndDashboard;
using Shared.SpecificationParameters.ReportsAndDashboard;

namespace Presentation.Controllers.DashboardAndReportsModule
{
    public class DashboardAndReportsController(IServiceManager _serviceManager) : ApiController
    {
        [ResponseCache]
        [Authorize(Policy = "Permission:Reports.Read")]
        [ProducesResponseType(typeof(AdminDashboardResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("Dashboard")]
            public async Task<ActionResult<AdminDashboardResponseDTO>> GetDashboardData()
                => Ok(await _serviceManager.DashboardService.GetAdminDashboardDataAsync());

        [ResponseCache]
        [Authorize(Policy = "Permission:Reports.Read")]
        [ProducesResponseType(typeof(ResearchesDashboardDTO), StatusCodes.Status200OK)]
        [HttpGet("ResearchesDashboard")]
        public async Task<ActionResult<ResearchesDashboardDTO>> GetResearchesDashboardData([FromQuery] ResearchesDashboardSpecificationParameters parameters)
        => Ok(await _serviceManager.DashboardService.GetResearchDashboardDataAsync(parameters));
    }
}
