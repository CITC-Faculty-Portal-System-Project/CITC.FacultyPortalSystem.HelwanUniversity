using Shared.Enums.MessagingAndChattingModule;

namespace Shared.Dtos.MessagingAndChattingModule
{
    public record CreateConverstationDTO
    {
        public ConversationType Type { get; set; }
        public string? Title { get; set; }
        public int? TicketId { get; set; }


        public List<ConverstationParticipantsDTO>? Participants { get; set; }

    }
}
