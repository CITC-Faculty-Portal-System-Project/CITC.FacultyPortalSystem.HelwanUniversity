using Domain.Entities.AdminModule;

namespace Domain.Entities.Messaging
{
    public class Conversation : BaseEntity<int>
    {
        public int? TicketId { get; set; }
        public ConversationType Type { get; set; }
        public string? Title { get; set; }
        public DateTime LastMessageAt { get; set; }

        #region NavigationsAndRelationShips
        public Ticket? Ticket { get; set; }

        public ICollection<ConversationParticipant> Participants { get; set; } = new List<ConversationParticipant>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();


        #endregion

    }
}
