using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Shared.Dtos.HigherStudiesModule;

namespace Services.MappingProfiles
{
    public class HigherStudiesModuleProfile : Profile
    {
        public HigherStudiesModuleProfile()
        {
            CreateMap<ThesesCreateDTO, Thesis>();
            CreateMap<SupervisorCreateDTO , Supervisor>();
            CreateMap<SupervisingCreateDTO, Supervising>();
        }
    }
}
