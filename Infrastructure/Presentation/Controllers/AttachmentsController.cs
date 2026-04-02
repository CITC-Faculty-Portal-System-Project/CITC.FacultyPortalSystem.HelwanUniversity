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
        [HttpPost("{entityId}")]
        public async Task<ActionResult<IEnumerable<AttachmentResponseDTO>>> UploadAttachment(
            AttachmentContext context , int entityId, IList<IFormFile> files)
            => Ok(await _serviceManager.AttachmentService.AddAsync(context , entityId, files));

        [ProducesResponseType(typeof(AttachmentResponseDTO), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(BlockMaliciousExtensionsFilter))]
        [HttpPut("{entityId}/{oldAttachmentId}")]
        public async Task<ActionResult<AttachmentResponseDTO>> ReplaceAttachment
            (AttachmentContext context , int entityId, Guid oldAttachmentId, IFormFile newAttachment)

            => Ok(await _serviceManager.AttachmentService.UpdateAsync
                (context , entityId, oldAttachmentId, newAttachment));

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{entityId}/{attachmentId}")]
        public async Task<ActionResult> RemoveAttachment
            (AttachmentContext context, int entityId, Guid attachmentId)
        {
            await _serviceManager.AttachmentService.DeleteAsync
                (context , entityId, attachmentId);
            return NoContent();
        }

        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [HttpGet("{entityId}/{attachmentId}")]
        public async Task<ActionResult> DownloadAttachment
            (AttachmentContext context, int entityId, Guid attachmentId)
        {
            var attachment = await _serviceManager.AttachmentService.GetAsync
                (context, entityId, attachmentId);
            return new FileContentResult(attachment.AttachmentData, "application/octet-stream")
            {
                FileDownloadName = attachment.FileName
            };
        }


    }
}