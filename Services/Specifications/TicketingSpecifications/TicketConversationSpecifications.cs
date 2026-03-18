using Domain.Entities.AdminModule;
using Domain.Entities.Messaging;
using System.Linq.Expressions;

namespace Services.Specifications.TicketingSpecifications
{
    internal class TicketConversationSpecifications : BaseSpecifications<Conversation, int>
    {
        public TicketConversationSpecifications
            (int ticketId) 
            : base(c => c.TicketId == ticketId && !c.IsDeleted)
        {
            AddIncludes(c => c.Participants);
            AddIncludes(c => c.Messages);
            AddOrderByDescending(c => c.Messages
                           .OrderByDescending(m => m.CreatedAt)
                           .Select(m => m.CreatedAt)
                           .FirstOrDefault());

            AddIncludes(c => c.Ticket!);
        }
    }
}
