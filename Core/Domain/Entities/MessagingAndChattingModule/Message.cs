using Domain.Entities.EntitesAttachments;

namespace Domain.Entities.Messaging
{
    public class Message : BaseEntity<long>
    {
        public int ConversationId { get; set; }
        public Guid SenderId { get; set; }
        public string SenderUsername { get; set; } = string.Empty;
        public Guid RecieverId { get; set; }
        public string RecieverUsername { get; set; } = string.Empty;
        public byte[]? Ciphertext { get; set; } = [];
        public byte[]? Nonce { get; set; } = [];
        public byte[]? Tag { get; set; } = [];
        public MessageType Type { get; set; }
        public DateTime? DeleiverdAt { get; set; }
        public DateTime? ReadAt { get; set; }

        public int KeyVersion { get; set; }
        public string Algorithm { get; set; } = "AES-256-GCM";

        #region NavigationsAndRelations

        public Conversation? Conversation { get; set; }

        #endregion

    }
}
