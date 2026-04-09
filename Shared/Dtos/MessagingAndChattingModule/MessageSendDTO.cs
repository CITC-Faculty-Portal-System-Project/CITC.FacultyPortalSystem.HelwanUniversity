using Shared.Enums.MessagingAndChattingModule;

namespace Shared.Dtos.MessagingAndChattingModule
{
    public record MessageSendDTO
    {
        public int ConversationId { get; set; }
        public Guid RecieverId { get; set; }
        public string? Content { get; set; }
        public List<Guid>? AttachmentIds { get; set; }
        public MessageType MessageType { get; set; } = MessageType.Text;

    }
}
