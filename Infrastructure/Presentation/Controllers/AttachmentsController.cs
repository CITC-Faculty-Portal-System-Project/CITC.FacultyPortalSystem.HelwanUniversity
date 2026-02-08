using Microsoft.AspNetCore.Authorization;
using Presentation.Filters;
using Shared.Dtos.AttachmentsModule;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Services.Abstraction.Enums;

namespace Presentation.Controllers
{
    [Authorize]
    public class AttachmentsController(IServiceManager _serviceManager) : ApiController
    {
        [ProducesResponseType(typeof(IEnumerable<AttachmentResponseDTO>), StatusCodes.Status201Created)]
        [ServiceFilter(typeof(BlockMaliciousExtensionsFilter))]
        [HttpPost("UploadAttachment")]
        public async Task<ActionResult<IEnumerable<AttachmentResponseDTO>>> UploadAttachment(
            AttachmentContext context , int ownerId , IList<IFormFile> files)
            => Ok(await _serviceManager.AttachmentService.AddAsync(context , ownerId , files));

        [ProducesResponseType(typeof(AttachmentResponseDTO), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(BlockMaliciousExtensionsFilter))]
        [HttpPut("ReplaceAttachment/{oldAttachmentId}")]
        public async Task<ActionResult<AttachmentResponseDTO>> ReplaceAttachment
            (AttachmentContext context , int ownerId , Guid oldAttachmentId , IFormFile newAttachment)

            => Ok(await _serviceManager.AttachmentService.UpdateAsync
                (context , ownerId , oldAttachmentId, newAttachment));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("RemoveAttachment/{attachmentId}")]
        public async Task<ActionResult> RemoveAttachment
            (AttachmentContext context, int ownerId, Guid attachmentId)
        {
            await _serviceManager.AttachmentService.DeleteAsync(context , ownerId , attachmentId);
            return NoContent();
        }

        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [HttpGet("DownloadAttachment/{attachmentId}")]
        public async Task<ActionResult> DownloadAttachment
            (AttachmentContext context, int ownerId, Guid attachmentId)
        {
            var attachment = await _serviceManager.AttachmentService.GetAsync(context, ownerId, attachmentId);
            return new FileContentResult(attachment.AttachmentData, "application/octet-stream")
            {
                FileDownloadName = attachment.FileName
            };
        }


    }
}