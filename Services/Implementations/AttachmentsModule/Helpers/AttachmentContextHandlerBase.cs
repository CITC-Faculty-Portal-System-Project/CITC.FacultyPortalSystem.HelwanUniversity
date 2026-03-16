using Microsoft.AspNetCore.Http;
using Services.Abstraction.Contracts.AttachmentsModule.Helpers;
using Services.Abstraction.Enums;
using Shared.Dtos.AttachmentsModule;

namespace Services.Implementations.AttachmentsModule.Helpers
{
    public abstract class AttachmentContextHandlerBase<TAttachment> : IAttachmentContextHandler
    where TAttachment : BaseAttachmentEntity, new()
    {
        protected readonly AttachmentCore _svc;

        protected AttachmentContextHandlerBase(AttachmentCore svc) => _svc = svc;

        public abstract AttachmentContext Context { get; }
        protected abstract void SetOwner(TAttachment a, int ownerId);
        protected abstract bool MatchOwner(TAttachment a, int ownerId);
        protected virtual string GetOwnerFolder(TAttachment a) => ownerIdToFolder(GetOwnerId(a));
        protected abstract int GetOwnerId(TAttachment a);
        protected virtual string ownerIdToFolder(int id) => id.ToString();

        public Task<IReadOnlyList<AttachmentResponseDTO>> AddAsync(int ownerId, IList<IFormFile> files, CancellationToken ct)
            => _svc.AddInternalAsync<TAttachment>(ownerId, files , SetOwner, ct);

        public Task<AttachmentDownloadDTO> GetAsync(int ownerId, Guid attachmentId, CancellationToken ct)
            => _svc.GetInternalAsync<TAttachment>(ownerId, attachmentId, MatchOwner, ct);

        public Task<AttachmentResponseDTO> UpdateAsync(int ownerId, Guid attachmentId, IFormFile newFile, CancellationToken ct)
            => _svc.UpdateInternalAsync<TAttachment>(
                ownerId, attachmentId, newFile,
                matchOwner: MatchOwner,
                getOwnerFolder: a => GetOwnerFolder(a),
                ct);

        public Task DeleteAsync(int ownerId, Guid attachmentId, CancellationToken ct)
            => _svc.DeleteInternalAsync<TAttachment>(ownerId, attachmentId, MatchOwner, ct);
    }
}
