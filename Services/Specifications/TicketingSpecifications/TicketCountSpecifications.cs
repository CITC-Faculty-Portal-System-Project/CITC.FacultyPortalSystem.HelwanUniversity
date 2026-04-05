using Domain.Entities.AdminModule;
using Shared.SpecificationParameters.TicketingModule;
using System.Linq.Expressions;

namespace Services.Specifications.TicketingSpecifications
{
    internal class TicketCountSpecifications : BaseSpecifications<Ticket, int>
    {
        public TicketCountSpecifications
            (TicketSepcificationParameters parameters)
            : base(t =>
                !t.IsDeleted &&
                (
                    string.IsNullOrEmpty(parameters.Search) ||
                    t.Title!.Contains(parameters.Search) ||
                    t.Description!.Contains(parameters.Search)
                ) &&
                (
                    !parameters.Type.HasValue ||
                    t.Type == (Domain.Enums.TicketType)parameters.Type.Value
                ) &&
                (
                    !parameters.Status.HasValue ||
                    t.Status == (Domain.Enums.TicketStatus)parameters.Status.Value
                ) &&
                (
                    !parameters.Priority.HasValue ||
                    t.Priority == (Domain.Enums.TicketPriority)parameters.Priority.Value
                )
            )
        {
        }
    }
}
