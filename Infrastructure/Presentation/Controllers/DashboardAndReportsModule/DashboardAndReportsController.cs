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
        public async Task<ActionResult<IReadOnlyList<TopFiveResearchersStatsDTO>>> GetFacultyTopResearchersDashboardData([FromQuery]int FacultyIdTopFiveResearchers)
            => Ok(await _serviceManager.DashboardService.GetFacultyTopResearchersDashboardDataAsync(FacultyIdTopFiveResearchers));

        [Authorize(Policy = "Permission:Reports.Read")]
        [ProducesResponseType(typeof(IReadOnlyList<ResearchDepartmentStatsDTO>), StatusCodes.Status200OK)]
        [HttpGet("DepartmentResearchesDashboard")]
        public async Task<ActionResult<IReadOnlyList<ResearchDepartmentStatsDTO>>> GetDepartmentResearchesDashboardData([FromQuery] int FacultyIdDepartmentResearches)
            => Ok(await _serviceManager.DashboardService.GetDepartmentResearchesDashboardDataAsync(FacultyIdDepartmentResearches));

        [Authorize(Policy = "Permission:Reports.Read")]
        [ProducesResponseType(typeof(IReadOnlyList<DepartmentResearchersStatsDTO>), StatusCodes.Status200OK)]
        [HttpGet("DepartmentResearchersDashboard")]
        public async Task<ActionResult<IReadOnlyList<DepartmentResearchersStatsDTO>>> GetDepartmentResearchersDashboardData([FromQuery] int FacultyIdDepartmentResearchers)
            => Ok(await _serviceManager.DashboardService.GetDepartmentResearchersDashboardDataAsync(FacultyIdDepartmentResearchers));

        [Authorize(Policy = "Permission:Reports.Read")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpPost("FacultyResearchesAndResearchersReportPreview")]
        public async Task<ActionResult<string>> GetFacultyResearchesAndResearchersReportPreview([FromQuery] int FacultyIdFacultyResearchesReportPreview , ReportGenerationDTO notes)
           => Ok(await _serviceManager.ReportsPreviewingService.PreviewFacultyResearchesAndResearchersReportAsync(FacultyIdFacultyResearchesReportPreview , notes.Notes));

        [Authorize(Policy = "Permission:Reports.Read")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpPost("TotalUniversityResearchesReportPreview")]
        public async Task<ActionResult<string>> GetTotalUniversityResearchesReportPreview(ReportGenerationDTO notes)
          => Ok(await _serviceManager.ReportsPreviewingService.PreviewResearchesReportAsync(notes.Notes));


        [Authorize(Policy = "Permission:Reports.Read")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpPost("OverallSystemPerformanceReportPreview")]
        public async Task<ActionResult<string>> GetOverallSystemPerformanceReportPreview(ReportGenerationDTO notes)
         => Ok(await _serviceManager.ReportsPreviewingService.PreviewGeneralSystemInfoReportAsync(notes.Notes));

        [Authorize(Policy = "Permission:Reports.Read")]
        [HttpPost("DownloadFacultyResearchesReportPdf")]
        public async Task<IActionResult> DownloadFacultyResearchesReportPdf([FromQuery] int facultyId, [FromBody] ReportGenerationDTO notes)
        {
            var pdf = await _serviceManager.ReportsPDFGenerationService.GenerateFacultyResearchesReportAsync(facultyId, notes.Notes);
            return File(pdf, "application/pdf", "FacultyResearchesReport.pdf");
        }


        [Authorize(Policy = "Permission:Reports.Read")]
        [HttpPost("DownloadGeneralSystemReportPdf")]
        public async Task<IActionResult> DownloadGeneralSystemReportPdf([FromBody] ReportGenerationDTO notes)
        {
            var pdf = await _serviceManager.ReportsPDFGenerationService.GenerateAdminDashboardReportAsync(notes.Notes);
            return File(pdf, "application/pdf", "GeneralSystemReport.pdf");
        }


        [Authorize(Policy = "Permission:Reports.Read")]
        [HttpPost("DownloadResearchesReportPdf")]
        public async Task<IActionResult> DownloadResearchesReportPdf([FromBody] ReportGenerationDTO notes)
        {
            var pdf = await _serviceManager.ReportsPDFGenerationService.GenerateResearchDashboardReportAsync(notes.Notes);
            return File(pdf, "application/pdf", "GenerateResearchesReport.pdf");
        }
    }
}
