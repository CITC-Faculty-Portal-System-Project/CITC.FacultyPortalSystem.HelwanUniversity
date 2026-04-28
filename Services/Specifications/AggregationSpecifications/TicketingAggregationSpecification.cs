using Domain.Entities.AdminModule;
using Domain.Enums;
using Shared.Dtos.ReportsAndDashboard;

namespace Services.Specifications.AggregationSpecifications
{
    public class TicketingAggregationSpecification
        : AggregationSpecification<Ticket, TicketsStatsDTO>
    {
        public TicketingAggregationSpecification()
        {
            SetCriteria(t => true); 
        }

        public override IQueryable<TicketsStatsDTO> Apply(IQueryable<Ticket> query)
        {
            var filtered = query.Where(Criteria!);

            var openedCount = filtered.Count(t => t.Status == TicketStatus.Opened);

            var closedCount = filtered.Count(t => t.Status == TicketStatus.Closed);

            var modulesStats = filtered
                .GroupBy(t => t.Type)
                .Select(g => new ModulesProblemStatsDTO
                {
                    ModuleName = g.Key.ToString(),
                    NumberOfProblems = g.Count()
                })
                .ToList();

            var priorityStats = filtered
                .GroupBy(t => t.Priority)
                .Select(g => new TicketsPriorityStatsDTO
                {
                    PriorityName = (Shared.Enums.TicketingModule.TicketPriority)g.Key,
                    NumberOfTickets = g.Count()
                })
                .ToList();

            var result = new TicketsStatsDTO
            {
                OpenedTicketsNo = openedCount,
                ClosedTicketsNo = closedCount,
                ModulesProblems = modulesStats,
                TicketsPriorityStats = priorityStats
            };

            return new List<TicketsStatsDTO> { result }.AsQueryable();
        }
    }
}