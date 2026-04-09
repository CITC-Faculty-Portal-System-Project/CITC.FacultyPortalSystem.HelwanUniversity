using Domain.Entities.EntitesAttachments;
using Services.Abstraction.Enums;

namespace Services.Implementations.AttachmentsModule.Helpers.Handlers
{
    public sealed class ConversationAttachmentHandler
     : AttachmentContextHandlerBase<ConversationAttachment>
    {
        public ConversationAttachmentHandler(AttachmentCore svc) : base(svc) { }

        public override AttachmentContext Context
            => AttachmentContext.Conversation;

        protected override void SetOwner(ConversationAttachment a, int ownerId)
            => a.ConversationId = ownerId;

        protected override bool MatchOwner(ConversationAttachment a, int ownerId)
            => a.ConversationId == ownerId;

        protected override int GetOwnerId(ConversationAttachment a)
            => Convert.ToInt32(a.ConversationId);
    }
}
