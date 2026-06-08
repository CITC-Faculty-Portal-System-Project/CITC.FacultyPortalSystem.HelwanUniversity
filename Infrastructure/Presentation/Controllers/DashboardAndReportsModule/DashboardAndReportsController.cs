using Microsoft.AspNetCore.Authorization;
using Shared;
using Shared.Dtos.ReportsAndDashboard;
using Shared.Dtos.ReportsAndDashboard.ConferencesAndSeminarsModule;
using Shared.Dtos.ReportsAndDashboard.CVModule;
using Shared.Dtos.ReportsAndDashboard.ExpereincesModule;
using Shared.Dtos.ReportsAndDashboard.FacultyMemberDataModule;
using Shared.Dtos.ReportsAndDashboard.ProjectsAndComiteesModule;
using Shared.Dtos.ReportsAndDashboard.ResearchesModule;
using Shared.Dtos.ReportsAndDashboard.ReviewingArticlesModule;
using Shared.Dtos.ReportsAndDashboard.WrtingsAndPatentsModule;
using Shared.Dtos.ReportsAndDashboard.WrtingsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.ConferencesAndSeminarsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.CVModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.ExperienceModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.FacultyMembersDataModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.ProjectsAndComiteesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.ResearchesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.ReviewingArticlesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.WritingsAndPatentsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.WritingsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ConferencesAndSeminarsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.CVModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ExperincesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.FacultyMembersDataModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ProjectsAndComiteesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ResearchesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.ReviewingArticlesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.WritingsAndPatentsModule;
using Shared.SpecificationParameters.ReportsAndDashboard.Tables.WritingsModule;

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
        [HttpGet("FacultyResearchesAndResearchersReportPreview")]
        public async Task<ActionResult<string>> GetFacultyResearchesAndResearchersReportPreview([FromQuery] int FacultyIdFacultyResearchesReportPreview , [FromQuery] string? notes)
           => Ok(await _serviceManager.ReportsPreviewingService.PreviewFacultyResearchesAndResearchersReportAsync(FacultyIdFacultyResearchesReportPreview , notes));

        [Authorize(Policy = "Permission:Reports.Read")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpGet("TotalUniversityResearchesReportPreview")]
        public async Task<ActionResult<string>> GetTotalUniversityResearchesReportPreview([FromQuery] string? notes)
          => Ok(await _serviceManager.ReportsPreviewingService.PreviewResearchesReportAsync(notes));


        [Authorize(Policy = "Permission:Reports.Read")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpGet("OverallSystemPerformanceReportPreview")]
        public async Task<ActionResult<string>> GetOverallSystemPerformanceReportPreview([FromQuery] string? notes)
         => Ok(await _serviceManager.ReportsPreviewingService.PreviewGeneralSystemInfoReportAsync(notes));

        [Authorize(Policy = "Permission:Reports.Read")]
        [HttpGet("DownloadFacultyResearchesReportPdf")]
        public async Task<IActionResult> DownloadFacultyResearchesReportPdf([FromQuery] int facultyId, [FromQuery] string? notes)
        {
            var pdf = await _serviceManager.ReportsPDFGenerationService.GenerateFacultyResearchesReportAsync(facultyId, notes);
            return File(pdf, "application/pdf", "FacultyResearchesReport.pdf");
        }


        [Authorize(Policy = "Permission:Reports.Read")]
        [HttpGet("DownloadGeneralSystemReportPdf")]
        public async Task<IActionResult> DownloadGeneralSystemReportPdf([FromQuery] string? notes)
        {
            var pdf = await _serviceManager.ReportsPDFGenerationService.GenerateAdminDashboardReportAsync(notes);
            return File(pdf, "application/pdf", "GeneralSystemReport.pdf");
        }


        [Authorize(Policy = "Permission:Reports.Read")]
        [HttpGet("DownloadResearchesReportPdf")]
        public async Task<IActionResult> DownloadResearchesReportPdf([FromQuery] string? notes)
        {
            var pdf = await _serviceManager.ReportsPDFGenerationService.GenerateResearchDashboardReportAsync(notes);
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
        [Authorize(Policy = "Permission:FacultyMemberData.Read")]
        [Authorize(Policy = "Permission:FacultyMemberPrizesData.Read")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Read")]
        [Authorize(Policy = "Permission:FacultyMemberResearchesData.Read")]
        [HttpGet("FacultyMembersDataReportPDF")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetFacultyMembersReportPDF([FromQuery] FacultyMembersDataReportPdfSpecificationParameters parameters , string? notes)
        {
            var pdf = await _serviceManager.ReportsPDFGenerationService.GenerateFacultyMembersReportAsync(parameters, notes);
            return File(pdf, "application/pdf", "FacultyMembersDataReport.pdf");
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
        [Authorize(Policy = "Permission:FacultyMemberData.Read")]
        [HttpGet("FacultyMembersResearchesReportPDF")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetFacultyMembersResearchesReportPDF([FromQuery] FacultyMembersResearchesPdfSpecificationParameters parameters, string? notes)
        {
            var pdf = await _serviceManager.ReportsPDFGenerationService.GenerateFacultyMembersResearchesReportAsync(parameters, notes);
            return File(pdf, "application/pdf", "FacultyMembersResearchesReport.pdf");
        }


        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberResearchesData.Read")]
        [HttpGet("ResearchesPerYearReportTable")]
        [ProducesResponseType(typeof(PaginatedResult<ResearchesPerYearReportResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResult<ResearchesPerYearReportResponseDTO>>> GetResearchesPerYearReportTable([FromQuery] ResearchesPerYearReportSpecificationParameters parameters)
        {
            return Ok(await _serviceManager.ReportsDataService.GetResearchesPeryearReportAsync(parameters));
        }

        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberResearchesData.Read")]
        [HttpGet("ResearchesPerYearReportPDF")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetResearchesPerYearReportPDF([FromQuery] ResearchesPerYearPdfReportSpecificationParameters parameters, string? notes)
        {
            var pdf = await _serviceManager.ReportsPDFGenerationService.GenerateResearchesPerYearReportAsync(parameters, notes);
            return File(pdf, "application/pdf", "ResearchesPerYearReport.pdf");
        }


        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Read")]
        [HttpGet("ConferencesAndSeminarsReportTable")]
        [ProducesResponseType(typeof(PaginatedResult<ConferenceAndSeminarsReportResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResult<ConferenceAndSeminarsReportResponseDTO>>> GetConferencesAndSeminarsReportTable([FromQuery] ConferencesAndSeminarsReportSpecificationParameters parameters)
        {
            return Ok(await _serviceManager.ReportsDataService.GetConferencesAndSeminarsReportAsync(parameters));
        }


        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberMissionsData.Read")]
        [HttpGet("ConferencesAndSeminarsReportPDF")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetConferencesAndSeminarsReportPDF([FromQuery] ConferencesAndSeminarsReportPdfSpecificationParameters parameters, string? notes)
        {
            var pdf = await _serviceManager.ReportsPDFGenerationService.GenerateConferencesAndSeminarsReportAsync(parameters, notes);
            return File(pdf, "application/pdf", "ConferencesAndSeminarsReport.pdf");
        }


        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Read")]
        [HttpGet("WritingsReportTable")]
        [ProducesResponseType(typeof(PaginatedResult<WritingsReportResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResult<WritingsReportResponseDTO>>> GetWritingsReportTable([FromQuery] WritingsReportSpecificationParameters parameters)
        {
            return Ok(await _serviceManager.ReportsDataService.GetWritingsReportAsync(parameters));
        }

        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Read")]
        [HttpGet("WritingsReportPDF")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetWritingsReportPDF([FromQuery] WritingsReportPdfSpecificationParameters parameters, string? notes)
        {
            var pdf = await _serviceManager.ReportsPDFGenerationService.GenerateWritingsReportAsync(parameters, notes);
            return File(pdf, "application/pdf", "WritingsReport.pdf");
        }


        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberResearchesData.Read")]
        [Authorize(Policy = "Permission:FacultyMemberData.Read")]
        [HttpGet("CVsReportTable")]
        [ProducesResponseType(typeof(PaginatedResult<CVReportResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResult<CVReportResponseDTO>>> GetCvsReportTable([FromQuery] CVTableReportSpecificationParameters parameters)
        {
            return Ok(await _serviceManager.ReportsDataService.GetCvsReportAsync(parameters));
        }


        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberResearchesData.Read")]
        [Authorize(Policy = "Permission:FacultyMemberData.Read")]
        [HttpGet("CVsReportPDF")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetCvsReportPDF([FromQuery] CVPdfReportSpecificationParameters parameters, string? notes)
        {
            var pdf = await _serviceManager.ReportsPDFGenerationService.GenerateCvReportAsync(parameters, notes);
            return File(pdf, "application/pdf", "CVsReport.pdf");
        }


        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Read")]
        [HttpGet("ExperinceReportTable")]
        [ProducesResponseType(typeof(PaginatedResult<ExpereinceReportResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResult<ExpereinceReportResponseDTO>>> GetExperinceTable([FromQuery] ExperinceReportTableSpecificationParameters parameters)
        {
            return Ok(await _serviceManager.ReportsDataService.GetExperiencesReportAsync(parameters));
        }


        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberExperincesData.Read")]
        [HttpGet("ExperinceReportPDF")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetExperincePDF([FromQuery] ExperienceReportPdfSpecificationParameters parameters, string? notes)
        {
            var pdf = await _serviceManager.ReportsPDFGenerationService.GenerateExperienceReportAsync(parameters, notes);
            return File(pdf, "application/pdf", "ExperiencesReport.pdf");
        }


        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Read")]
        [HttpGet("ReviewingArticlesReportTable")]
        [ProducesResponseType(typeof(PaginatedResult<ReviewingArticlesReportResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResult<ReviewingArticlesReportResponseDTO>>> GetReviewingArticlesReport([FromQuery] ReviewingArticlesReportTableSpecificationParameters parameters)
        {
            return Ok (await _serviceManager.ReportsDataService.GetReviewingArticlesReportAsync(parameters));
        }


        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberContributionsData.Read")]
        [HttpGet("ReviewingArticlesReportPDF")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetReviewingArticlesReportPDF([FromQuery] ReviewingArticlesReportPDFSpecificationParameters parameters, string? notes)
        {
            var pdf = await _serviceManager.ReportsPDFGenerationService.GenerateReviewingArticlesReportAsync(parameters, notes);
            return File(pdf, "application/pdf", "ReviewingArticlesReport.pdf");
        }



        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Read")]
        [HttpGet("PatentsReportTable")]
        [ProducesResponseType(typeof(PaginatedResult<PatentsReportReponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResult<PatentsReportReponseDTO>>> GetPatentsReportTable([FromQuery] PatentsReportTableSpecificationParameters parameters)
        {
            return Ok(await _serviceManager.ReportsDataService.GetPatentsReportAsync(parameters));
        }


        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberWritingsData.Read")]
        [HttpGet("PatentsReportPDF")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetPatentsReportPdf([FromQuery] PatentsReportPdfSpecificationParameters parameters , string? notes)
        {
            var pdf = await _serviceManager.ReportsPDFGenerationService.GeneratePatentsReportAsync(parameters , notes);
            return File(pdf, "application/pdf", "PatentsReport.pdf");
        }


        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndComiteesData.Create")]
        [HttpGet("ProjectsReportTable")]
        [ProducesResponseType(typeof(PaginatedResult<ProjectsReportResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResult<ProjectsReportResponseDTO>>> GetProjectsReportTable([FromQuery] ProjectsReportTableSpecificationParameters parameters)
        {
            return Ok(await _serviceManager.ReportsDataService.GetProjectsReportAsync(parameters));
        }


        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndComiteesData.Create")]
        [HttpGet("ProjectsReportPDF")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetProjectsPdfReport([FromQuery] ProjectsReportPdfSpecificationParameters parameters , string? notes)
        {
            var pdf = await _serviceManager.ReportsPDFGenerationService.GenerateProjectsReportAsync(parameters , notes);
            return File(pdf, "application/pdf", "ProjectsReport.pdf");
        }


        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndComiteesData.Create")]
        [HttpGet("ParticipationInMagazinesReportTable")]
        [ProducesResponseType(typeof(PaginatedResult<ParticipationInMagazinesReportResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResult<ParticipationInMagazinesReportResponseDTO>>> GetParticipationInMagazinesTable([FromQuery] ParticipationInMagazinesReportTableSpecificationParameters parameters)
        {
            return Ok(await _serviceManager.ReportsDataService.GetParticipationInMagazinesReportAsync(parameters));
        }


        [Authorize(Policy = "Permission:Reports.Read")]
        [Authorize(Policy = "Permission:FacultyMemberProjectsAndComiteesData.Create")]
        [HttpGet("ParticipationInMagazinesReportPDF")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetParticipationInMagazinesReportPDF([FromQuery] ParticipationInMagazinesPdfReportSpecificationParamters parameters, string? notes)
        {
            var pdf = await _serviceManager.ReportsPDFGenerationService.GenerateParticipationInMagazinesReportAsync(parameters, notes);
            return File(pdf, "application/pdf", "ParticipationInMagazinesReport.pdf");
        }

    }
}