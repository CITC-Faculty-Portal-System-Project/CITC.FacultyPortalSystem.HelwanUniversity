using Shared.Enums.MessagingAndChattingModule;

namespace Shared.Dtos.MessagingAndChattingModule
{
    public class MessageResponseDTO
    {
        public long Id { get; set; }
        public int ConversationId { get; set; }
        public Guid SenderId { get; set; }
        public Guid RecieverId { get; set; }
        public string SenderUsername { get; set; } = string.Empty;
        public string RecieverUsername { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<Guid>? AttachmentsIds { get; set; }
        public MessageType MessageType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeleiverdAt { get; set; }
        public DateTime? ReadAt { get; set; }

    }
}
