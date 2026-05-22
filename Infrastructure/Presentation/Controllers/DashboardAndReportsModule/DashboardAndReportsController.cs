using Microsoft.AspNetCore.Authorization;
using Shared;
using Shared.Dtos.ReportsAndDashboard;
using Shared.Dtos.ReportsAndDashboard.FacultyMemberDataModule;
using Shared.Dtos.ReportsAndDashboard.ResearchesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.FacultyMembersDataModule;
using Shared.SpecificationParameters.ReportsAndDashboard.ResearchesModule;

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


        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberData.Read")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Read")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Read")]
        [Authorize(Policy = "Permission:FacultyMemberResearchesData.Read")]
        [HttpGet("FacultyMembersDataReportTable")]
        [ProducesResponseType(typeof(PaginatedResult<FacultyMembersDataReportResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResult<FacultyMembersDataReportResponseDTO>>> GetFacultyMembersReportTable([FromQuery] FacultyMembersDataReportSpecificatonParameters parameters)
        {
            return Ok(await _serviceManager.ReportsDataService.GetFacultyMembersDataReportAsync(parameters));
        }

        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberResearchesData.Read")]
        [Authorize(Policy = "Permission:FacultyMemberData.Read")]
        [HttpGet("FacultyMembersResearchesReportTable")]
        [ProducesResponseType(typeof(PaginatedResult<FacultyMembersResearchesReportResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResult<FacultyMembersResearchesReportResponseDTO>>> GetFacultyMembersResearchesReportTable([FromQuery] FacultyMembersResearchesSpecificationParameters parameters)
        {
            return Ok(await _serviceManager.ReportsDataService.GetFacultyMembersResearchesReportAsync(parameters));
        }

        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberResearchesData.Read")]
        [HttpGet("ResearchesPerYearReportTable")]
        [ProducesResponseType(typeof(PaginatedResult<ResearchesPerYearReportResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResult<ResearchesPerYearReportResponseDTO>>> GetResearchesPerYearReportTable([FromQuery] ResearchesPerYearReportSpecificationParameters parameters)
        {
            return Ok(await _serviceManager.ReportsDataService.GetResearchesPeryearReportAsync(parameters));
        }


    }
}