using Domain.Entities.AcademicDataModule.ExperiencesModule;
using Domain.Entities.EntitesAttachments;
using Domain.Entities.FacultyMemberDataModule;
using Shared.Dtos.AttachmentsModule;
using Shared.Dtos.FacultyMemberDataModule;

namespace Services.MappingProfiles
{
    public class FacultyMemberDataProfile : Profile
    {
        public FacultyMemberDataProfile()
        {
            CreateMap<PersonalData, PersonalDataResponseDto>()
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
               .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
               .ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => src.MaritalStatus))
               .ForMember(dest => dest.University, opt => opt.MapFrom(src => src.University))
               .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
               .ForMember(dest => dest.Authority, opt => opt.MapFrom(src => src.Authority))
               .ForMember(dest => dest.Field, opt => opt.MapFrom(src => src.Field));

            CreateMap<PersonalDataUpdateDto, PersonalData>()
               .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
               {
                   if (srcMember == null) return false;               
                   if (srcMember is Guid g) return g != Guid.Empty;   
                   return true;
               }));

            CreateMap<PersonalDataCreateDTO, PersonalData>();

            CreateMap<ContactData, ContactDataResponseDto>();
            CreateMap<ContactDataCreateDTO, ContactData>();
            CreateMap<ContactDataUpdateDto, ContactData>();
            CreateMap<AttachmentReferenceDTO, ProfilePictures>();
            CreateMap<ProfilePictures, AttachmentResponseDTO>();

            CreateMap<IdentificationCardDto, IdentificationCard>().ReverseMap();

            CreateMap<SocialMediaPlatformsDto, SocialMediaPlatforms>().ReverseMap();

            #region Profile Dashboard
            CreateMap<PersonalData, ProfileDashboardResponseDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.University, opt => opt.MapFrom(src => src.University))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.NameAr))
                .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameAr))
                .ForMember(dest => dest.BioSummary, opt => opt.MapFrom(src => src.BioSummary))
                .ForMember(dest => dest.Skills,
                opt => opt.MapFrom(src =>
                    string.IsNullOrWhiteSpace(src.Skills)
                        ? new List<string>()
                        : src.Skills.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                        ));

            CreateMap<SkillsDTO, PersonalData>()
                .ForMember(dest => dest.Skills,
                opt => opt.MapFrom(src =>
                    src.Skills != null && src.Skills.Any()
                        ? string.Join(";", src.Skills)
                        : string.Empty
                ));
            #endregion
        }
    }
}
