namespace Shared.Dtos.TicketingModule
{
    public record TicketUpdateDTO
    {
        public Guid? AssignedToId { get; set; }
        public string? AssigneeUsername { get; set; }
        public Guid? AssignedById { get; set; }
        public string? AssignedByUsername { get; set; }

    }
}
