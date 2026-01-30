using Microsoft.AspNetCore.Authorization;
using Presentation.Filters;
using Shared.Dtos.AttachmentsModule;
using Shared.Dtos.FacultyMemberDataModule;

namespace Presentation.Controllers
{
    [Authorize]
    public class AttachmentsController(IServiceManager _serviceManager) : ApiController
    {
        [ProducesResponseType(typeof(IEnumerable<AttachmentResponseDTO>), StatusCodes.Status201Created)]
        [ServiceFilter(typeof(BlockMaliciousExtensionsFilter))]
        [HttpPost("UploadAttachment")]
        public async Task<ActionResult<IEnumerable<AttachmentResponseDTO>>> UploadAttachment(IList<IFormFile> attachments)
            => Ok(await _serviceManager.AttachmentService.AddAttachmentAsync(attachments));

        [ProducesResponseType(typeof(AttachmentResponseDTO), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(BlockMaliciousExtensionsFilter))]
        [HttpPut("ReplaceAttachment/{oldAttachmentId}")]
        public async Task<ActionResult<AttachmentResponseDTO>> ReplaceAttachment(Guid oldAttachmentId 
            , IFormFile newAttachment)
            
            => Ok(await _serviceManager.AttachmentService.UpdateAttachmentAsync(oldAttachmentId , newAttachment));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("RemoveAttachment/{attachmentId}")]
        public async Task<ActionResult> RemoveAttachment(Guid attachmentId)
        {
            await _serviceManager.AttachmentService.DeleteAttachmentAsync(attachmentId);
            return NoContent();
        }

        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [HttpGet("DownloadAttachment/{attachmentId}")]
        public async Task<ActionResult> DownloadAttachment(Guid attachmentId)
        {
            var attachment = await _serviceManager.AttachmentService.GetAttachmentAsync(attachmentId);
            return new FileContentResult(attachment.AttachmentData, "application/octet-stream")
            {
                FileDownloadName = attachment.FileName
            };
        }
            

    }
}
