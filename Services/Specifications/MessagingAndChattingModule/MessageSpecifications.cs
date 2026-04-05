using Domain.Entities.Messaging;
using Microsoft.EntityFrameworkCore;
using Shared.SpecificationParameters.MessagingAndChattingModule;

namespace Services.Specifications.MessagingAndChattingModule
{
    internal class MessageSpecifications : BaseSpecifications<Message, long>
    {
        public MessageSpecifications
            (MessageSpecificationParameters parameters)
            : base(m => !m.IsDeleted && m.ConversationId == parameters.ConversationId
            &&
                (!parameters.BeforeMessageId.HasValue || m.Id < parameters.BeforeMessageId.Value))
        {

            ApplyCursorTake(parameters.Take); 
            AddOrderByDescending(m => m.Id);
            AddIncludeWithChain(m =>
                m.Include(m => m.Conversation)
                .ThenInclude(c => c!.Participants));
        }


        public MessageSpecifications(long messageId)
        : base(m => !m.IsDeleted && m.Id == messageId)
        {

            AddIncludeWithChain(m =>
              m.Include(m => m.Conversation)
              .ThenInclude(c => c!.Participants));

        }
    }
}
