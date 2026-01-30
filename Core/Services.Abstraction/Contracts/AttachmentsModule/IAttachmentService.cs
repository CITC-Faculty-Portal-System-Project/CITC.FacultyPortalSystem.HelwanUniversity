using Microsoft.AspNetCore.Http;
using Shared.Dtos.AttachmentsModule;

namespace Services.Abstraction.Contracts.AttachmentsModule
{
    public interface IAttachmentService
    {
        Task<IEnumerable<AttachmentResponseDTO>> AddAttachmentAsync(IList<IFormFile> files);
        Task<AttachmentDownloadDTO> GetAttachmentAsync(Guid attachmentId);
        Task<AttachmentResponseDTO> UpdateAttachmentAsync(Guid oldAttachmentId , IFormFile newFile);
        Task DeleteAttachmentAsync(Guid attachmentId);
    }
}
