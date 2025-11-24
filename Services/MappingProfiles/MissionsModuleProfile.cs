using Domain.Entities.MissionsModule;
using Shared.Dtos.MissionsModule;

namespace Services.MappingProfiles
{
    public class MissionsModuleProfile : Profile
    {
        public MissionsModuleProfile()
        {
            #region Scientific Missions
            CreateMap<ScientificMissions, ScientificMissionResponseDto>();

            CreateMap<ScientificMissionCreateDto, ScientificMissions>();
            CreateMap<ScientificMissionUpdateDto, ScientificMissions>();
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
