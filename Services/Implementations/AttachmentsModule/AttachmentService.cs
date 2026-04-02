using Domain.Contracts;
using Domain.Entities.EntitesAttachments;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Http;
using Services.Abstraction.Contracts.AttachmentsModule;
using Services.Abstraction.Contracts.AttachmentsModule.Helpers;
using Services.Abstraction.Enums;
using Shared.Dtos.AttachmentsModule;
using System.Net.Mail;

namespace Services.Implementations.AttachmentsModule
{
    public class AttachmentService(
        IEnumerable<IAttachmentContextHandler> handlers
    ) : IAttachmentService
    {
        private readonly IReadOnlyDictionary<AttachmentContext, IAttachmentContextHandler> _handlers =
            handlers.ToDictionary(h => h.Context);

        private IAttachmentContextHandler Handler(AttachmentContext context)
            => _handlers.TryGetValue(context, out var h)
                ? h
                : throw new ArgumentOutOfRangeException(nameof(context));

        public Task<IReadOnlyList<AttachmentResponseDTO>> AddAsync(
            AttachmentContext context, int ownerId, IList<IFormFile> files, CancellationToken ct = default)
            => Handler(context).AddAsync(ownerId, files, ct);

        public Task<AttachmentDownloadDTO> GetAsync(
            AttachmentContext context, int ownerId, Guid attachmentId, CancellationToken ct = default)
            => Handler(context).GetAsync(ownerId, attachmentId, ct);

        public Task<AttachmentResponseDTO> UpdateAsync(
            AttachmentContext context, int ownerId, Guid attachmentId, IFormFile newFile, CancellationToken ct = default)
            => Handler(context).UpdateAsync(ownerId, attachmentId, newFile, ct);

        public Task DeleteAsync(
            AttachmentContext context, int ownerId, Guid attachmentId, CancellationToken ct = default)
            => Handler(context).DeleteAsync(ownerId, attachmentId, ct);
    }
}
