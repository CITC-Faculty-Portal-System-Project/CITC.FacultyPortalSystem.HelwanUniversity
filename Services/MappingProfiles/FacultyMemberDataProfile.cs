using Domain.Entities.FacultyMemberDataModule;
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

            CreateMap<PersonalDataCreateDto, PersonalData>();
            CreateMap<PersonalDataUpdateDto, PersonalData>();

            CreateMap<ContactData, ContactDataResponseDto>();
            CreateMap<ContactDataCreateDto, ContactData>();
            CreateMap<ContactDataUpdateDto, ContactData>();

            CreateMap<IdentificationCardDto, IdentificationCard>().ReverseMap();

            CreateMap<SocialMediaPlatformsDto, SocialMediaPlatforms>().ReverseMap();
        }
    }
}
