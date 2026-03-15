using Domain.Entities.Messaging;

namespace Domain.Entities.AdminModule
{
    public class Ticket : BaseEntity<int>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketType Type { get; set; }
        public TicketPriority Priority { get; set; }
        public TicketStatus Status { get; set; }
        public Guid SenderId { get; set; }
        public string SenderUsername { get; set; } = string.Empty;
        public Guid? AssignedToId { get; set; }
        public string? AssigneeUsername { get; set; }
        public Guid? AssignedById { get; set; }
        public string? AssignedByUsername { get; set; }

        #region NavigationsAndRelations
        [NotMapped]
        public Conversation? Conversation { get; set; }
        #endregion
    }
}
