using Domain.Entities.AcademicDataModule.ContributionsModule;
using Domain.Entities.AcademicDataModule.ExperiencesModule;
using Domain.Entities.AcademicDataModule.MissionsModule;
using Domain.Entities.AcademicDataModule.PrizesModule;
using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Domain.Entities.AcademicDataModule.WritingsAndPatents;
using Domain.Entities.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Dtos.CVGenerationModule.Contributions;
using Shared.Dtos.CVGenerationModule.Experiences;
using Shared.Dtos.CVGenerationModule.Missions;
using Shared.Dtos.CVGenerationModule.Prizes;
using Shared.Dtos.CVGenerationModule.ProjectsAndCommittees;
using Shared.Dtos.CVGenerationModule.ScientificProgression;
using Shared.Dtos.CVGenerationModule.WritingsAndPatents;
using Shared.Dtos.FacultyMemberDataModule;

namespace Services.MappingProfiles
{
    public class CVMappingProfile : Profile
    {
        public CVMappingProfile()
        {
            CreateMap<PersonalData, CVResponseDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.University, opt => opt.MapFrom(src => src.University))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.Authority, opt => opt.MapFrom(src => src.Authority))
                //.ForMember(dest => dest.ProfilePictureId, opt => opt.MapFrom(src => src.ProfilePictureId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BioSummary, opt => opt.MapFrom(src => src.BioSummary))
                .ForMember(dest => dest.Skills,
                opt => opt.MapFrom(src =>
                    string.IsNullOrWhiteSpace(src.Skills)
                        ? new List<string>()
                        : src.Skills.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                ));

            CreateMap<AcademicQualifications, CVAcademicQualificationsDTO>()
                .ForMember(dest => dest.Qualification, opt => opt.MapFrom(src => src.Qualification))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade))
                .ForMember(dest => dest.DispatchType, opt => opt.MapFrom(src => src.DispatchType));

            CreateMap<JobRanks, CVJobRanksDTO>()
                .ForMember(dest => dest.JobRank, opt => opt.MapFrom(src => src.JobRank));

            CreateMap<AdministrativePositions, CVAdministrativePositions>();

            CreateMap<ConferencesAndSeminars, CVConferencesAndSeminarsDTO>()
                .ForMember(dest => dest.RoleOfParticipation, opt => opt.MapFrom(src => src.RoleOfParticipation));

            CreateMap<ScientificMissions, CVScientificMissionsDTO>();

            CreateMap<TrainingPrograms, CVTrainingProgramsDTO>();

            CreateMap<CommitteesAndAssociations, CVCommitteesAndAssociationsDTO>()
                .ForMember(dest => dest.DegreeOfSubscription, opt => opt.MapFrom(src => src.DegreeOfSubscription))
                .ForMember(dest => dest.TypeOfCommitteeOrAssociation, opt => opt.MapFrom(src => src.TypeOfCommitteeOrAssociation));

            CreateMap<ParticipationInMagazines, CVParticipationInMagazinesDTO>()
                .ForMember(dest => dest.TypeOfParticipation, opt => opt.MapFrom(src => src.TypeOfParticipation));

            CreateMap<ReviewingArticles, CVReviewingArticlesDTO>();

            CreateMap<Projects, CVProjectsDTO>()
                .ForMember(dest => dest.ParticipationRole, opt => opt.MapFrom(src => src.ParticipationRole))
                .ForMember(dest => dest.TypeOfProject, opt => opt.MapFrom(src => src.TypeOfProject));

            CreateMap<GeneralExperiences, CVGeneralExperienceDTO>();

            CreateMap<TeachingExperiences, CVTeachingExperienceDTO>();

            CreateMap<ScientificWritings, CVScientificWritingDTO>()
                .ForMember(dest => dest.AuthorRole, opt => opt.MapFrom(src => src.AuthorRole));

            CreateMap<Patents, CVPatentDTO>();

            CreateMap<PrizesAndRewards, CVPrizesAndRewardsDTO>()
                .ForMember(dest => dest.Prize, opt => opt.MapFrom(src => src.Prize));

            CreateMap<ManifestationsOfScientificAppreciation, CVManifestationsOfScientificAppreciationDTO>();

            CreateMap<ContributionsToCommunityService, CVContributionsToCommunityServiceDTO>();

            CreateMap<ContributionsToUniversity, CVContributionsToUniversityDTO>()
                .ForMember(dest => dest.TypeOfContribution, opt => opt.MapFrom(src => src.TypeOfContribution));

            CreateMap<ParticipationInQualityWorks, CVParticipationInQualityWorkDTO>();

            CreateMap<CVVisibilitySettings, CVVisibilitySettingResponseDTO>()
                .ForMember(dest => dest.VisibilityJson, opt => opt.MapFrom(src => src.VisibilityJson));
        }
    }
}
