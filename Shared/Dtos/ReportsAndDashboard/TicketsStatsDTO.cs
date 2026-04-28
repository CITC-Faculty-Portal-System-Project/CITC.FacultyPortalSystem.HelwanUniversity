namespace Shared.Dtos.ReportsAndDashboard
{
    public record TicketsStatsDTO
    {
        public int OpenedTicketsNo { get; set; }
        public int ClosedTicketsNo { get; set; }
        public IReadOnlyList<ModulesProblemStatsDTO> ModulesProblems { get; set; } = new List<ModulesProblemStatsDTO>();
        public IReadOnlyList<TicketsPriorityStatsDTO> TicketsPriorityStats { get; set; } = new List<TicketsPriorityStatsDTO>();
    }
}
