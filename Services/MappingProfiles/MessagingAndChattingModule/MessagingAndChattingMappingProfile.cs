using Domain.Entities.Messaging;
using Shared.Dtos.MessagingAndChattingModule;

namespace Services.MappingProfiles.MessagingAndChattingModule
{
    public class MessagingAndChattingMappingProfile : Profile
    {
        public MessagingAndChattingMappingProfile()
        {
            CreateMap<MessageSendDTO, Message>()
              .ForMember(dest => dest.Conversation, opt => opt.Ignore());


            CreateMap<Message, MessageSendDTO>();
            CreateMap<Message, MessageResponseDTO>();
            CreateMap<Message, EncryptedMessageResult>();
            CreateMap<EncryptedMessageResult, Message>();

            CreateMap<ConverstationParticipantsDTO, ConversationParticipant>();
            CreateMap<ConversationParticipant, ConverstationParticipantsDTO>();

            //CreateMap<CreateConverstationDTO, Conversation>();
            CreateMap<Conversation, ConversationResponseDTO>();
            CreateMap<CreateConverstationDTO, Conversation>();

           
        }
    }
}
