using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Shared.Dtos.AcademicDataModule.HigherStudiesModule;

namespace Services.MappingProfiles.AcademicDataModule
{
    public class HigherStudiesModuleProfile : Profile
    {
        public HigherStudiesModuleProfile()
        {
            CreateMap<ThesesCreateDTO, Thesis>();
            CreateMap<SupervisorCreateDTO , ThesisComittee>();
            CreateMap<SupervisingCreateDTO, Supervising>();
        }
    }
}
