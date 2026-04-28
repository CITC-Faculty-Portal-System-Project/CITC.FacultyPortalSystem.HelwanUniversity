using Shared.Enums.TicketingModule;

namespace Shared.Dtos.ReportsAndDashboard
{
    public record TicketsPriorityStatsDTO
    {
        public TicketPriority PriorityName { get; set; }
        public int NumberOfTickets { get; set; }
    }
}
