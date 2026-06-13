using Domain.Entities.AcademicDataModule.ExperiencesModule;
using Shared.Dtos.FacultyMemberDataModule;
using Shared.Dtos.FacultyMembersProfilesModule;

namespace Services.MappingProfiles
{
    public class FacultyMembersPublicProfileMappingProfile : Profile
    {
        public FacultyMembersPublicProfileMappingProfile() {
            CreateMap<FacultyMember, OtherUsersPageResponseDTO>()
             .ForMember(dest => dest.FacultyMemberName, opt => opt.MapFrom(src => src.PersonalData!.Title.ValueAr + src.PersonalData.NameAr))
             .ForMember(dest => dest.PersonalDataId, opt => opt.MapFrom(src => src.PersonalData!.Id))
             .ForMember(dest => dest.FacultyMemberEmail, opt => opt.MapFrom(src => src.Email))
             .ForMember(dest => dest.FacultyMemberPosition, opt => opt.MapFrom(src => src.PersonalData!.Title.ValueAr))
             .ForMember(dest => dest.FacultyMemberDepartment, opt => opt.MapFrom(src => src.PersonalData!.Department.NameAR))
             .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.PersonalData!.ProfilePicture));



            CreateMap<FacultyMember, FacultyMemberPublicProfileResponseDTO>()
             .ForMember(dest => dest.FacultyMemberName, opt => opt.MapFrom(src => src.PersonalData!.Title.ValueAr + src.PersonalData.NameAr))
              .ForMember(dest => dest.FacultyMemberEmail, opt => opt.MapFrom(src => src.Email))

             .ForMember(dest => dest.Interests, opt => opt.MapFrom(src => src.Researcher!.ResearcherInterests!.Select(i => i.Interest)))
             .ForMember(dest => dest.PersonalDataId, opt => opt.MapFrom(src => src.PersonalData!.Id))
             .ForMember(dest => dest.BioSummary, opt => opt.MapFrom(src => src.PersonalData!.BioSummary))
             .ForMember(dest => dest.RegisterationId, opt => opt.MapFrom(src => "Mem - " + src.CreatedAt.Date.Year + src.CreatedAt.Date.Month 
                            + src.CreatedAt.Date.Day + src.CreatedAt.Date.Hour + src.CreatedAt.Date.Minute))
             
             
             .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(src => !src.IsDeleted))
             .ForMember(dest => dest.IssueDate, opt => opt.MapFrom(src => src.CreatedAt.Date))
             .ForMember(dest => dest.Researches, opt => opt.MapFrom(src => src.ResearchContributions!.Select(rc => rc.Research)))
                .ForMember(
                    dest => dest.Experinces,
                    opt => opt.MapFrom(src =>
                        src.GeneralExperiences.Cast<object>()
                            .Concat(src.TeachingExperiences))
                )
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.PersonalData!.ProfilePicture));


            CreateMap<GeneralExperiences, ExperiencesSummaryDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.ExperienceTitle))
                .ForMember(dest => dest.Organization, opt => opt.MapFrom(src => src.Authority));
           
            
            CreateMap<TeachingExperiences, ExperiencesSummaryDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.CourseName))
                .ForMember(dest => dest.Organization, opt => opt.MapFrom(src => src.UniversityOrFaculty));

        }
    }
}
