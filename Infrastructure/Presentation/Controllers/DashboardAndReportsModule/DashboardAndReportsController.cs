using Microsoft.AspNetCore.Authorization;
using Shared.Dtos.ReportsAndDashboard;
using Shared.SpecificationParameters.ReportsAndDashboard;

namespace Presentation.Controllers.DashboardAndReportsModule
{
    public class DashboardAndReportsController(IServiceManager _serviceManager) : ApiController
    {
        [Authorize(Policy = "Permission:Reports.Read")]
        [ProducesResponseType(typeof(AdminDashboardResponseDTO), StatusCodes.Status200OK)]
        [HttpGet("Dashboard")]
            public async Task<ActionResult<AdminDashboardResponseDTO>> GetDashboardData()
                => Ok(await _serviceManager.DashboardService.GetAdminDashboardDataAsync());

        [Authorize(Policy = "Permission:Reports.Read")]
        [ProducesResponseType(typeof(ResearchesDashboardDTO), StatusCodes.Status200OK)]
        [HttpGet("ResearchesDashboard")]
        public async Task<ActionResult<ResearchesDashboardDTO>> GetResearchesDashboardData()
        => Ok(await _serviceManager.DashboardService.GetResearchDashboardDataAsync());

        [Authorize(Policy = "Permission:Reports.Read")]
        [ProducesResponseType(typeof(IReadOnlyList<TopFiveResearchersStatsDTO>), StatusCodes.Status200OK)]
        [HttpGet("FacultyTopResearchersDashboard")]
        public async Task<ActionResult<IReadOnlyList<TopFiveResearchersStatsDTO>>> GetFacultyTopResearchersDashboardData([FromQuery]ResearchersPerFacultySpecificationParameters researchersPerFacultySpecificationParameters)
            => Ok(await _serviceManager.DashboardService.GetFacultyTopResearchersDashboardDataAsync(researchersPerFacultySpecificationParameters));

        [Authorize(Policy = "Permission:Reports.Read")]
        [ProducesResponseType(typeof(IReadOnlyList<ResearchDepartmentStatsDTO>), StatusCodes.Status200OK)]
        [HttpGet("DepartmentResearchesDashboard")]
        public async Task<ActionResult<IReadOnlyList<ResearchDepartmentStatsDTO>>> GetDepartmentResearchesDashboardData([FromQuery] ResearchesPerDepartmentSpecificationParameters researchesPerDepartmentSpecificationParameters)
            => Ok(await _serviceManager.DashboardService.GetDepartmentResearchesDashboardDataAsync(researchesPerDepartmentSpecificationParameters));

        [Authorize(Policy = "Permission:Reports.Read")]
        [ProducesResponseType(typeof(IReadOnlyList<DepartmentResearchersStatsDTO>), StatusCodes.Status200OK)]
        [HttpGet("DepartmentResearchersDashboard")]
        public async Task<ActionResult<IReadOnlyList<DepartmentResearchersStatsDTO>>> GetDepartmentResearchersDashboardData([FromQuery] ResearchersPerDepartmentSpecificationParameters researchersPerDepartmentSpecificationParameters)
            => Ok(await _serviceManager.DashboardService.GetDepartmentResearchersDashboardDataAsync(researchersPerDepartmentSpecificationParameters));
    }
}
