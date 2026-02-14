using Domain.Entities.AcademicDataModule.ContributionsModule;
using Shared.Dtos.AcademicDataModule.ContributionsModule;

namespace Services.MappingProfiles.AcademicDataModule
{
    public class ContributionsModuleProfile : Profile
    {
        public ContributionsModuleProfile()
        {
            #region ContributionsToCommunityService
            CreateMap<ContributionsToCommunityService, ContributionsToCommunityServiceResponseDTO>();
            CreateMap<ContributionsToCommunityServiceCreateDTO, ContributionsToCommunityService>();
            CreateMap<ContributionsToCommunityServiceUpdateDTO, ContributionsToCommunityService>();
            #endregion

            #region ContributionsToUniversity
            CreateMap<ContributionsToUniversity, ContributionsToUniversityResponseDTO>()
                .ForMember(dest => dest.TypeOfContribution, opt => opt.MapFrom(src => src.TypeOfContribution));
            CreateMap<ContributionsToUniversityCreateDTO, ContributionsToUniversity>();
            CreateMap<ContributionsToUniversityUpdateDTO, ContributionsToUniversity>();
            #endregion

            #region ParticipationInQualityWorks
            CreateMap<ParticipationInQualityWorks, ParticipationInQualityWorksResponseDTO>();
            CreateMap<ParticipationInQualityWorksCreateDTO, ParticipationInQualityWorks>();
            CreateMap<ParticipationInQualityWorksUpdateDTO, ParticipationInQualityWorks>();
            #endregion
        }
    }
}
