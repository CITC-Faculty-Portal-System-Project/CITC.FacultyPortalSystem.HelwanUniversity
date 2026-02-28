using Microsoft.AspNetCore.Http;
using Services.Abstraction.Enums;
using Shared.Dtos.AttachmentsModule;

namespace Services.Abstraction.Contracts.AttachmentsModule.Helpers
{
    public interface IAttachmentContextHandler
    {
        AttachmentContext Context { get; }

        Task<IReadOnlyList<AttachmentResponseDTO>> AddAsync(
            int ownerId, IList<IFormFile> files, CancellationToken ct);

        Task<AttachmentDownloadDTO> GetAsync(
            int ownerId, Guid attachmentId, CancellationToken ct);

        Task<AttachmentResponseDTO> UpdateAsync(
            int ownerId, Guid attachmentId, IFormFile newFile, CancellationToken ct);

        Task DeleteAsync(
            int ownerId, Guid attachmentId, CancellationToken ct);
    }
}
