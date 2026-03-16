using Domain.Entities.EntitesAttachments;
using Services.Abstraction.Enums;

namespace Services.Implementations.AttachmentsModule.Helpers.Handlers
{
    public sealed class ConferenceOrSeminarAttachmentHandler
        : AttachmentContextHandlerBase<ConferencesAndSeminarsAttachment>
    {
        public ConferenceOrSeminarAttachmentHandler(AttachmentCore svc) : base(svc) { }

        public override AttachmentContext Context => AttachmentContext.ConferenceOrSeminar;

        protected override void SetOwner(ConferencesAndSeminarsAttachment a, int ownerId)
            => a.ConferenceOrSeminarId = ownerId;

        protected override bool MatchOwner(ConferencesAndSeminarsAttachment a, int ownerId)
            => a.ConferenceOrSeminarId == ownerId;

        protected override int GetOwnerId(ConferencesAndSeminarsAttachment a)
            => a.ConferenceOrSeminarId;
    }
}
