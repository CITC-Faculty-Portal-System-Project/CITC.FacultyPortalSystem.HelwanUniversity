using Shared.Enums.TicketingModule;

namespace Shared.Dtos.TicketingModule
{
    public record TicketResponseDTO
    {

        public int Id { get; set; }
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

    }
}
