
using Domain.Entities.EntitesAttachments;
using Services.Abstraction.Enums;

namespace Services.Implementations.AttachmentsModule.Helpers.Handlers
{
    public sealed class ResearchAttachmentHandler : AttachmentContextHandlerBase<ResearchAttachment>
    {
        public ResearchAttachmentHandler(AttachmentCore svc) : base(svc) { }

        public override AttachmentContext Context => AttachmentContext.Research;

        protected override void SetOwner(ResearchAttachment a, int ownerId) => a.ResearchId = ownerId;
        protected override bool MatchOwner(ResearchAttachment a, int ownerId) => a.ResearchId == ownerId;
        protected override int GetOwnerId(ResearchAttachment a) => a.ResearchId;
    }
}
