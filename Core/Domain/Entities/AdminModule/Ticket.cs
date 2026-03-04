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
        public Guid? AssignedToId { get; set; }
        public ICollection<TicketMessage> Messages { get; set; }
            = new List<TicketMessage>();
    }
}
