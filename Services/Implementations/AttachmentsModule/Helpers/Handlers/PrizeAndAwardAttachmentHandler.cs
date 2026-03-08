
using Domain.Entities.EntitesAttachments;
using Services.Abstraction.Enums;

namespace Services.Implementations.AttachmentsModule.Helpers.Handlers
{
    public sealed class PrizeAndAwardAttachmentHandler
        : AttachmentContextHandlerBase<PrizesAndAwardsAttachment>
    {
        public PrizeAndAwardAttachmentHandler(AttachmentCore svc) : base(svc) { }

        public override AttachmentContext Context => AttachmentContext.PrizeAndAward;

        protected override void SetOwner(PrizesAndAwardsAttachment a, int ownerId)
            => a.PrizeAndAwardId = ownerId;

        protected override bool MatchOwner(PrizesAndAwardsAttachment a, int ownerId)
            => a.PrizeAndAwardId == ownerId;

        protected override int GetOwnerId(PrizesAndAwardsAttachment a)
            => a.PrizeAndAwardId;
    }
}
