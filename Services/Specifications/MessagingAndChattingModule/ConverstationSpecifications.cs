using Domain.Entities.AdminModule;
using Domain.Entities.Messaging;
using System.Linq.Expressions;

namespace Services.Specifications.MessagingAndChattingModule
{
    internal class ConverstationSpecifications : BaseSpecifications<Conversation, int>
    {
        public ConverstationSpecifications
            (int Id) 
            :base(c => c.Id == Id && !c.IsDeleted)
        {
            AddIncludes(c => c.Participants);
        }
    }
}
