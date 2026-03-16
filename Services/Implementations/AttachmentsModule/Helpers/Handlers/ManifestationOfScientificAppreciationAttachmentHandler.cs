using Domain.Entities.EntitesAttachments;
using Services.Abstraction.Enums;

namespace Services.Implementations.AttachmentsModule.Helpers.Handlers
{
    public sealed class ManifestationOfScientificAppreciationAttachmentHandler
    : AttachmentContextHandlerBase<ManifestationsOfScientificAppreciationAttachment>
    {
        public ManifestationOfScientificAppreciationAttachmentHandler(AttachmentCore svc) : base(svc) { }

        public override AttachmentContext Context
            => AttachmentContext.ManifestationOfScientificAppreciation;

        protected override void SetOwner(ManifestationsOfScientificAppreciationAttachment a, int ownerId)
            => a.ManifestationOfScientificAppreciationId = ownerId;

        protected override bool MatchOwner(ManifestationsOfScientificAppreciationAttachment a, int ownerId)
            => a.ManifestationOfScientificAppreciationId == ownerId;

        protected override int GetOwnerId(ManifestationsOfScientificAppreciationAttachment a)
            => a.ManifestationOfScientificAppreciationId;
    }
}
