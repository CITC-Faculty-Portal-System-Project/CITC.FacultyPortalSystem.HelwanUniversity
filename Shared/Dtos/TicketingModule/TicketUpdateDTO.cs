using Shared.Enums.TicketingModule;

namespace Shared.Dtos.TicketingModule
{
    public record TicketUpdateDTO
    {
        public TicketPriority Priority { get; set; }
        public Guid AssignedToId { get; set; }
        public string AssigneeUsername { get; set; } = string.Empty;
        public Guid AssignedById { get; set; }
        public string AssignedByUsername { get; set; } = string.Empty;

    }
}
