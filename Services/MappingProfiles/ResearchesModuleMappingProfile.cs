using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.EntitesAttachments;
using Shared.Dtos.AttachmentsModule;
using Shared.Dtos.DataFetchingFromExternalService;
using Shared.Dtos.ResearchesModule;

namespace Services.MappingProfiles
{
    public class ResearchesModuleMappingProfile : Profile
    {
        public ResearchesModuleMappingProfile() {
            CreateMap<ResearcherDataFetchingDTO, ResearcherProfile>()
                .ForMember(dest => dest.ResearcherCites, opt => opt.Ignore())
                .ForMember(dest => dest.ResearcherInterests, opt => opt.Ignore());


            CreateMap<ExternalResearcherInterestsFetchingDTO, ScientificInterest>();
            
            CreateMap<ResearcherCoAuthorFetchingDTO, CoAuthor>()
                .ForMember(dest => dest.Researchers, opt => opt.Ignore());

            CreateMap<CoAuthor, ResearcherCoAuthorFetchingDTO>();


            CreateMap<ExternalResearchCitesFetchingDTO, ResearchCite>();
            CreateMap<ExternalResearcherCitesFetchingDTO, ResearcherCite>();
            CreateMap<ExternalResearchesFetchingDTO, Research>()
                .ForMember(dest => dest.Contributions, opt => opt.Ignore())
                .ForMember(dest => dest.Cites, opt => opt.Ignore())
                .ForMember(dest => dest.JournalOrConfernce, opt => opt.MapFrom(src => src.Journal))
                .ForMember(dest => dest.Issue, opt => opt.MapFrom(src => src.Number));

            CreateMap<ExternalResearchContributionFetchingDTO, ResearchContribution>();
            CreateMap<ResearcherProfile, ResearcherProfileResponseDTO>();

            CreateMap<ResearcherProfile, ResearcherProfileResponseDTO>()
          .ForMember(dest => dest.ResearcherInterests,
              opt => opt.MapFrom(src =>
                  src.ResearcherInterests!.Select(ri => ri.Interest)))
           
          .ForMember(dest => dest.ResearcherCites,
              opt => opt.MapFrom(src => src.ResearcherCites));

            CreateMap<ScientificInterest, ExternalResearcherInterestsFetchingDTO>();
            CreateMap<ResearcherCite, ExternalResearcherCitesFetchingDTO>();

            CreateMap<SupervisingThesesAddDTO, Supervising>()
                  .ForMember(dest => dest.FacultyMember, opt => opt.Ignore())
                .ForMember(dest => dest.Grade, opt => opt.Ignore());


            CreateMap<Supervising, SupervisingThsesResponseDTO>();
            CreateMap<SupervisingThesesUpdateDTO ,  Supervising>()
               .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember, ctx) =>
               {
                   if (srcMember is null) return false;
                   if (srcMember is string s && string.IsNullOrWhiteSpace(s)) return false;
                   if (srcMember is Guid g && g == Guid.Empty) return false;
                   if (srcMember is DateTime dt && dt == default) return false;
                   if (srcMember is int i && i == default) return false;
                   if (srcMember is long l && l == default) return false;
                   if (srcMember is Enum e && Convert.ToInt32(e) == 0)
                       return false;

                   return true;
               }));

            CreateMap<ThesesDTO, Thesis>()
                 .ForMember(d => d.Attachments, opt => opt.Ignore())
                 .ForMember(d => d.Researches, opt => opt.Ignore());


            CreateMap<Thesis, ThesesResponseDTO>();


            CreateMap<ThesesSupervisorDTO, ThesisComittee>()
                    .ForMember(dest => dest.JobLevelId, opt => opt.MapFrom(src => src.JobLevelId));
                    
            CreateMap<ThesisComittee, ThesesSupervisorDTO>();

            CreateMap<ResearchDTO , Research>();
            CreateMap<ResearchContributionDTO , ResearchContribution>();
            CreateMap<Research, ResearchDTO>(); 
            CreateMap<Research, ResearchResponseDTO>();
            CreateMap<ResearchResponseDTO, ResearchDTO>();
            CreateMap<ThesesSupervisorResponseDTO, ThesesSupervisorDTO>();
            CreateMap<ThesesUpdateDTO, ThesesDTO>()
                .ForMember(dest => dest.ComitteeMembers, opt => opt.MapFrom(src => src.SupervisorsToAdd));
            

            CreateMap<ResearchResponseDTO, Research>()
                .ForMember(d => d.Id, opt => opt.Ignore())

                .ForMember(d => d.ThesisId, opt => opt.Ignore())
                .ForMember(d => d.Thesis, opt => opt.Ignore())

                .ForMember(d => d.Contributions, opt =>
                {
                    opt.PreCondition(s => s.Contributions != null);
                    opt.MapFrom(s => s.Contributions);
                })
                .ForMember(d => d.Cites, opt =>
                {
                    opt.PreCondition(s => s.Cites != null);
                    opt.MapFrom(s => s.Cites);
                })
                .ForMember(d => d.Attachments, opt =>
                {
                    opt.PreCondition(s => s.Attachments != null);
                    opt.MapFrom(s => s.Attachments);
                });

            CreateMap<ResearchContributionResponseDTO, ResearchContribution>()
                .ForMember(d => d.Id, opt => opt.Ignore()); 

            CreateMap<ResearchCitesResponseDTO, ResearchCite>()
                .ForMember(d => d.Id, opt => opt.Ignore());

            CreateMap<AttachmentResponseDTO, ResearchAttachment>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Research, opt => opt.Ignore())
                .ForMember(d => d.ResearchId, opt => opt.Ignore());

            CreateMap<ResearchContribution, ResearchContributionResponseDTO>();
            CreateMap<ResearchUpdateDTO, Research>();
            CreateMap<ResearchCite, ResearchCitesResponseDTO>();
            CreateMap<ThesesSupervisorDTO, ThesisComittee>();


            CreateMap<ThesisComittee, ThesesSupervisorResponseDTO>();
            CreateMap<ThesesUpdateDTO, Thesis>();

            CreateMap<ThesesDTO, SupervisingThesesAddDTO>()
                .ForMember(dest => dest.GrantingDate, opt => opt.MapFrom(src => src.InternalGradeDate))
                .ForMember(dest => dest.SupervisionFormationDate, opt => opt.MapFrom(src => src.SupervisionConfirmationDate))
                .ForMember(dest => dest.FacultyMemberId, opt => opt.Ignore());
        }
    }
}
