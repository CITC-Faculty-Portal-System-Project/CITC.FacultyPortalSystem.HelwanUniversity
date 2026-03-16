using Domain.Entities.EntitesAttachments;
using Services.Abstraction.Enums;

namespace Services.Implementations.AttachmentsModule.Helpers.Handlers
{
    public sealed class PatentAttachmentHandler
    : AttachmentContextHandlerBase<PatentsAttachment>
    {
        public PatentAttachmentHandler(AttachmentCore svc) : base(svc) { }

        public override AttachmentContext Context => AttachmentContext.Patent;

        protected override void SetOwner(PatentsAttachment a, int ownerId)
            => a.PatentId = ownerId;

        protected override bool MatchOwner(PatentsAttachment a, int ownerId)
            => a.PatentId == ownerId;

        protected override int GetOwnerId(PatentsAttachment a)
            => a.PatentId;
    }
}
