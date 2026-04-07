using Shared.Enums.TicketingModule;

namespace Shared.Dtos.TicketingModule
{
    public record TicketCreateDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketType Type { get; set; }
        public Guid SenderId { get; set; }
        public string SenderUsername { get; set; } = string.Empty;
      
    }
}
