using Domain.Entities.AcademicDataModule.WritingsAndPatents;
using Domain.Entities.Messaging;

namespace Domain.Entities.EntitesAttachments
{
    public class ConversationAttachment : BaseAttachmentEntity
    {
        public int ConversationId { get; set; }
        public Conversation? Conversation { get; set; }
        public void SetOwnerKey(object key) => ConversationId = Convert.ToInt32(key);

    }
}
