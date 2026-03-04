namespace Domain.Entities.AdminModule
{
    public class TicketMessage : BaseEntity<int>
    {
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!;
        public Guid SenderId { get; set; } 
        public string Message { get; set; } = string.Empty;

    }
}
