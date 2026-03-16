using Shared.Dtos.AcademicDataModule.HigherStudiesModule;
using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.DataFetchingFromExternalService;
using Shared.Dtos.FacultyMemberDataModule;
using Shared.Enums.AcademicDataModule.HigherStudiesModule;
using Shared.Enums.AcademicDataModule.MissionsModule;

namespace Services.MappingProfiles
{
    public class FetchingDataFromExternalServiceMappingProfile : Profile
    {
        public FetchingDataFromExternalServiceMappingProfile()
        {

            CreateMap<AcademicQualificationFetchingDTO, AcademicQualificationCreateDto>()
                .ForMember(dest => dest.CountryOrCity, opt => opt.MapFrom(src => src.CountryCity))
                .ForMember(dest => dest.DateOfObtainingTheQualification, opt => opt.MapFrom(src => DateOnly.Parse(src.DateOfAcquisition)))
                .ForMember(dest => dest.UniversityOrFaculty, opt => opt.MapFrom(src => src.UniversityFaculty));


            CreateMap<ContactDataFetchingDTO, ContactDataCreateDTO>();
            
            CreateMap<JobRanksFetchingDTO, JobRankCreateDto>()
                .ForMember(dest => dest.DateOfJobRank, opt => opt.MapFrom(src => src.PromotionDate));

            CreateMap<AdminstrativePostionsFetchingDTO, AdministrativePositionCreateDto>()
                   .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Name))
                   .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Description));

            CreateMap<PersonalDataFetchingDTO, PersonalDataCreateDTO>();
            
            CreateMap<SceintificMissionsFetchingDTO, ScientificMissionCreateDto>()
                .ForMember(dest => dest.MissionName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UniversityOrFaculty, opt => opt.MapFrom(src => src.UniversityFaculty))
                .ForMember(dest => dest.CountryOrCity, opt => opt.MapFrom(src => src.CountryCity));


            CreateMap<SupervisingsFetchingDTO, SupervisingCreateDTO>()
                  .ForMember(dest => dest.UniversityOrFaculty, opt => opt.MapFrom(src => src.UniversityFaculty))
                  .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.ThesisTitle))
                  .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ThesisType.Trim() == "ماجستير"
                                                                                ? ThesesType.Master
                                                                                : ThesesType.PHD))
                  
                  
                  .ForMember(dest => dest.FacultyMemberRole, opt => opt.MapFrom(src => src.Role.Contains("مشرف")
                                                    ? (src.Role.Contains("و مراجع")
                                                        ? FacultyMemberRoleInSupervisingThesis.AdminstratorAndReviewer
                                                        : FacultyMemberRoleInSupervisingThesis.Adminstrator)
                                                    : FacultyMemberRoleInSupervisingThesis.Reviewer));


            CreateMap<TrainingProgramsFetchingDTO, TrainingProgramsCreateDto>()
                    .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.ProgramPlace))
                    .ForMember(dest => dest.OrganizingAuthority, opt => opt.MapFrom(src => src.OrganizerName))
                    .ForMember(dest => dest.TrainingProgramName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.ParticipationType, opt => opt.MapFrom(src => src.ParticipationType
                                                                        .Trim() == "محاضر"
                                                                        ? TrainingProgramParticipationType.lecturer
                                                                        : TrainingProgramParticipationType.listener))
                    
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ProgramType.Trim() == "في التخصص"
                        ? TrainingProgramType.InTheSpecialty
                        : TrainingProgramType.OutTheSpecialty));


            CreateMap<ThesesFetchingDTO, ThesesCreateDTO>()
                     .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.Trim() == "رسالة الماجستير"
                                                                        ? ThesesType.Master
                                                                        : ThesesType.PHD));   



            CreateMap<ThesesSupervisorsFetchingDTO , SupervisorCreateDTO>()
                    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Trim() == "اشراف"
                                    ? SupervisorRole.Adminstration
                                    : SupervisorRole.Reviewing
                                                             ));


        }
    }
}
