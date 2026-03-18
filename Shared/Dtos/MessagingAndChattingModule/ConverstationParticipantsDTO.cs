namespace Shared.Dtos.MessagingAndChattingModule
{
    public record ConverstationParticipantsDTO
    {
        public int ConversationId { get; set; }
        public Guid UserId { get; set; }
        public DateTime JoinedAt { get; set; }
        public string Username { get; set; } = string.Empty;

    }
}
