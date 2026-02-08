using Microsoft.AspNetCore.Http;
using Services.Abstraction.Enums;
using Shared.Dtos.AttachmentsModule;
using System.Net.Mail;

namespace Services.Abstraction.Contracts.AttachmentsModule
{
    public interface IAttachmentService
    {
        Task<IReadOnlyList<AttachmentResponseDTO>> AddAsync(
              AttachmentContext context,
              int ownerId,
              IList<IFormFile> files,
              CancellationToken ct = default);

        Task<AttachmentDownloadDTO> GetAsync(
            AttachmentContext context,
            int ownerId,
            Guid attachmentId,
            CancellationToken ct = default);

        Task<AttachmentResponseDTO> UpdateAsync(
            AttachmentContext context,
            int ownerId,
            Guid attachmentId,
            IFormFile newFile,
            CancellationToken ct = default);

        Task DeleteAsync(
            AttachmentContext context,
            int ownerId,
            Guid attachmentId,
            CancellationToken ct = default);
     }
}
