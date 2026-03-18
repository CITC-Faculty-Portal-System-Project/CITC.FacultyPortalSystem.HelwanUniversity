using Domain.Entities.Messaging;
using Shared.SpecificationParameters.MessagingAndChattingModule;

namespace Services.Specifications.MessagingAndChattingModule
{
    internal class MessagesCountSpecifications : BaseSpecifications<Message, long>
    {
        public MessagesCountSpecifications(MessageSpecificationParameters parameters)
            : base(m =>
                m.ConversationId == parameters.ConversationId &&
                !m.IsDeleted)
        {
        }
    }
}
