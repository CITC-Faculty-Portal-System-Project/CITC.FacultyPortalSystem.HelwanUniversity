using Domain.Entities.EntitesAttachments;
using Services.Abstraction.Enums;

namespace Services.Implementations.AttachmentsModule.Helpers.Handlers
{
    public sealed class ThesisAttachmentHandler : AttachmentContextHandlerBase<ThesesAttachment>
    {
        public ThesisAttachmentHandler(AttachmentCore svc) : base(svc) { }

        public override AttachmentContext Context => AttachmentContext.Thesis;

        protected override void SetOwner(ThesesAttachment a, int ownerId) => a.ThesisId = ownerId;
        protected override bool MatchOwner(ThesesAttachment a, int ownerId) => a.ThesisId == ownerId;
        protected override int GetOwnerId(ThesesAttachment a) => a.ThesisId;
    }
}
