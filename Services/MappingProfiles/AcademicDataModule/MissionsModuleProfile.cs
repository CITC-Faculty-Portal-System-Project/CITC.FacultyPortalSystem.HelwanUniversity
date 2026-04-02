using Domain.Entities.AcademicDataModule.MissionsModule;
using Domain.Entities.EntitesAttachments;
using Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.Dtos.AttachmentsModule;

namespace Services.MappingProfiles.AcademicDataModule
{
    public class MissionsModuleProfile : Profile
    {
        public MissionsModuleProfile()
        {
            #region Scientific Missions
            
            CreateMap<ScientificMissions, ScientificMissionResponseDto>();

            CreateMap<ScientificMissionCreateDto, ScientificMissions>()
                .ForMember(dest => dest.FacultyMember, opt => opt.Ignore());
            
            CreateMap<ScientificMissionUpdateDto, ScientificMissions>()
                 .ForMember(dest => dest.FacultyMember, opt => opt.Ignore());

            #endregion

            #region Seminars And Conferences
           
            CreateMap<ConferencesAndSeminars, ConferencesAndSeminarsResponseDto>();


            CreateMap<ConferencesAndSeminarsCreateDto, ConferencesAndSeminars>()
                        .ForMember(dest => dest.FacultyMember, opt => opt.Ignore())
                        .ForMember(dest => dest.RoleOfParticipation, opt => opt.Ignore());
            
            CreateMap<AttachmentReferenceDTO, ConferencesAndSeminarsAttachment>();
            CreateMap<ConferencesAndSeminarsAttachment, AttachmentResponseDTO>();

            CreateMap<ConferencesAndSeminarsUpdateDto, ConferencesAndSeminars>()
                     .ForMember(dest => dest.FacultyMember, opt => opt.Ignore())
                     .ForMember(dest => dest.RoleOfParticipation, opt => opt.Ignore());



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
