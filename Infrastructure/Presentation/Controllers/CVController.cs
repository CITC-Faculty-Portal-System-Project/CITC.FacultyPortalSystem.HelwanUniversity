using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.CVGenerationModule;
using Shared.Dtos.FacultyMemberDataModule;
using Shared.Models.CVGenerationModule;

namespace Presentation.Controllers
{
    [Authorize]
    public class CVController(IServiceManager _serviceManager) : ApiController
    {
        [ProducesResponseType(typeof(CVResponseDTO), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<CVResponseDTO>> GetProfileDashboardAsync()
            => Ok(await _serviceManager.CVGenerationService.GetCVAsync());

        [ProducesResponseType(typeof(CVVisibilitySettingResponseDTO), StatusCodes.Status200OK)]
        [HttpPut("Manage-CV-Visibility")]
        public async Task<ActionResult<CVVisibilitySettingResponseDTO>> ManageVisibility(CVVisibilityConfig config)

            => Ok(await _serviceManager.CVGenerationService.ManageCVVisibilityAsync(config));


        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpGet("Get-Template")]
        public async Task<ActionResult<CVVisibilitySettingResponseDTO>> GetUserTemplate(Guid? userId)

         => Ok(await _serviceManager.CVGenerationService.GetUserPrefferedTemplate(userId));

        [HttpGet("Download-Pdf")]
        public async Task<IActionResult> DownloadPdf(string template = "modern" , bool isPublic = false)
        {
            var pdf = await _serviceManager.CVGenerationService.GenerateCVPdfAsync(template , isPublic);
            return File(pdf, "application/pdf", "CV.pdf");
        }

        [HttpGet("Preview")]
        public async Task<IActionResult> Preview(string template = "modern", bool isPublic = false)
        {
            var html = await _serviceManager.CVGenerationService.PreviewCVAsync(template , isPublic);
            return Content(html, "text/html");
        }

        [AllowAnonymous]
        [HttpGet("public/{id}")]
        public async Task<ActionResult<CVResponseDTO>> GetPublicCV(Guid id)
        {
            var cv = await _serviceManager.CVGenerationService.GetPublicCVAsync(id);
            return Ok(cv);
        }
    }
}
