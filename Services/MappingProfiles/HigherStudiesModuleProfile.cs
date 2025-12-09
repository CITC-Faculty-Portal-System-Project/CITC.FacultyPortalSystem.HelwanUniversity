using Domain.Entities.HigherStuidesModule;
using Shared.Dtos.HigherStudiesModule;

namespace Services.MappingProfiles
{
    public class HigherStudiesModuleProfile : Profile
    {
        public HigherStudiesModuleProfile()
        {
            CreateMap<ThesesCreateDTO, Thesis>();
            CreateMap<SupervisorCreateDTO , Supervisor>();
            CreateMap<Thesis, ThesesResponseDTO>();
            CreateMap<SupervisingCreateDTO, Supervising>();
        }
    }
}
