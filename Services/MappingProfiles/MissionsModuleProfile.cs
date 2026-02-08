using Domain.Entities.AcademicDataModule.MissionsModule;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Shared.Dtos.MissionsModule;

namespace Services.MappingProfiles
{
    public class MissionsModuleProfile : Profile
    {
        public MissionsModuleProfile()
        {
            #region Scientific Missions
            CreateMap<ScientificMissions, ScientificMissionResponseDto>();

            CreateMap<ScientificMissionCreateDto, ScientificMissions>()
         .ForMember(dest => dest.MissionName, opt => opt.MapFrom(src => src.name))
         .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Description));
            CreateMap<ScientificMissionUpdateDto, ScientificMissions>();

            CreateMap<ScientificMissionUpdateDto, ScientificMissionResponseDto>();
            #endregion

            #region Seminars And Conferences
            CreateMap<ConferencesAndSeminars, ConferencesAndSeminarsResponseDto>()
                 .ForMember(dest => dest.RoleOfParticipation, opt => opt.MapFrom(src => src.RoleOfParticipation))
                 .ForMember(dest => dest.LocalOrInternational, opt => opt.MapFrom(src => src.LocalOrInternational))
                 .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));

            CreateMap<ConferencesAndSeminarsCreateDto, ConferencesAndSeminars>();
    

            CreateMap<ConferencesAndSeminarsUpdateDto, ConferencesAndSeminars>();

            #endregion

            #region Training Programs
            CreateMap<TrainingPrograms, TrainingProgramsResponseDto>()
                .ForMember(dest => dest.ParticipationType, opt => opt.MapFrom(src => src.ParticipationType))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));

            CreateMap<TrainingProgramsCreateDto, TrainingPrograms>();
            CreateMap<TrainingProgramsUpdateDto, TrainingPrograms>();
            #endregion

        }
    }
}
