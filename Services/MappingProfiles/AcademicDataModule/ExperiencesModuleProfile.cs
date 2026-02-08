using Domain.Entities.AcademicDataModule.ExperiencesModule;
using Shared.Dtos.AcademicDataModule.ExperiencesModule;

namespace Services.MappingProfiles.AcademicDataModule
{
    public class ExperiencesModuleProfile : Profile
    {
        public ExperiencesModuleProfile()
        {
            #region General Experiences
            CreateMap<GeneralExperiences, GeneralExperiencesResponseDTO>();
            CreateMap<GeneralExperiencesCreateDTO, GeneralExperiences>();
            CreateMap<GeneralExperiencesUpdateDTO, GeneralExperiences>();
            #endregion

            #region Teaching Experiences
            CreateMap<TeachingExperiences, TeachingExperiencesResponseDTO>();
            CreateMap<TeachingExperiencesCreateDTO, TeachingExperiences>();
            CreateMap<TeachingExperiencesUpdateDTO, TeachingExperiences>();
            #endregion
        }
    }
}
