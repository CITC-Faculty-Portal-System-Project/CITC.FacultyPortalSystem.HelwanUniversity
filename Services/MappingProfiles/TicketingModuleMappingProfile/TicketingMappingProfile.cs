using Domain.Entities.AdminModule;
using Shared.Dtos.TicketingModule;

namespace Services.MappingProfiles.TicketingModuleMappingProfile
{
    public class TicketingMappingProfile : Profile
    {
        public TicketingMappingProfile() { 

            CreateMap<TicketCreateDTO , Ticket>()
               .ForMember(d => d.Conversation, opt => opt.Ignore());

            CreateMap<Ticket, TicketResponseDTO>();
            CreateMap<TicketUpdateDTO, Ticket>()
                .ForMember(d => d.Conversation, opt => opt.Ignore());

        }
    }
}
