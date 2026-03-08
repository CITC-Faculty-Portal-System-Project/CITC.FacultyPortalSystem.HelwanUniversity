using Domain.Contracts;
using FtpFileStorage.Configurations;
using Presistence.Repositories;
using Services.Abstraction.Contracts.AcademicDataModule.ContributionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.ExperiencesModule;
using Services.Abstraction.Contracts.AcademicDataModule.MissionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.PrizesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule;
using Services.Abstraction.Contracts.AcademicDataModule.WritingsAndPatentsModule;
using Services.Abstraction.Contracts.AdminModule;
using Services.Abstraction.Contracts.AttachmentsModule;
using Services.Abstraction.Contracts.AttachmentsModule.Helpers;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ContributionsModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ExperiencesModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.FacultyMemberDataModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.MissionsModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.PrizesModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.ScientificProgressionModule;
using Services.Abstraction.Contracts.SharedLogicBetweenAdminAndFacultyMember.WritingsAndPatentsModule;
using Services.Helpers.ExternalDataFetchingServiceHelpers;
using Services.Implementations.AcademicDataModule.ContributionsModule;
using Services.Implementations.AcademicDataModule.ExperiencesModule;
using Services.Implementations.AcademicDataModule.MissionsModule;
using Services.Implementations.AcademicDataModule.PrizesModule;
using Services.Implementations.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Implementations.AcademicDataModule.ResearchesModule;
using Services.Implementations.AcademicDataModule.ScientificProgressionModule;
using Services.Implementations.AcademicDataModule.WritingsAndPatentsModule;
using Services.Implementations.AdminModule;
using Services.Implementations.AttachmentsModule;
using Services.Implementations.AttachmentsModule.Helpers;
using Services.Implementations.AttachmentsModule.Helpers.Handlers;
using Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.AcademicDataModule.ContributionsModule;
using Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.ContributionsModule;
using Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.ExperiencesModule;
using Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.FacultyMemberDataModule;
using Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.MissionsModule;
using Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.PrizesModule;
using Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.ProjectsAndComiteesModule;
using Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.ScientificProgressionModule;
using Services.Implementations.SharedLogicBetweenAdminAndFacultyMember.WritingsAndPatentsModule;
using Shared.Common;

namespace ICIT.FacultyPortalSystem.API.Extensions
{
    public static class CoreServicesExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(cfg => { }, typeof(AssemblyReference).Assembly);
            services.AddScoped<IServiceManager, ServiceManager>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<Func<IAuthenticationService>>(provider =>
            () => provider.GetRequiredService<IAuthenticationService>()
            );

            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<Func<ICacheService>>(provider =>
            () => provider.GetRequiredService<ICacheService>()
            );

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<Func<IEmailService>>(provider =>
            () => provider.GetRequiredService<IEmailService>()
            );

            services.AddScoped<IFacultyMemberDataService, FacultyMemberDataService>();
            services.AddScoped<Func<IFacultyMemberDataService>>(provider =>
            () => provider.GetRequiredService<IFacultyMemberDataService>()
            );

            services.AddScoped<ILookUpItemService, LookUpItemService>();
            services.AddScoped<Func<ILookUpItemService>>(provider =>
            () => provider.GetRequiredService<ILookUpItemService>()
            );

            services.AddScoped<IScientificMissionsService, ScientificMissionsService>();
            services.AddScoped<Func<IScientificMissionsService>>(provider =>
            () => provider.GetRequiredService<IScientificMissionsService>()
            );

            services.AddScoped<ISeminarsAndConferencesService, SeminarsAndConferncesService>();
            services.AddScoped<Func<ISeminarsAndConferencesService>>(provider =>
            () => provider.GetRequiredService<ISeminarsAndConferencesService>()
            );

            services.AddScoped<ITrainingProgramsService, TrainingProgramsService>();
            services.AddScoped<Func<ITrainingProgramsService>>(provider =>
            () => provider.GetRequiredService<ITrainingProgramsService>()
            );

            services.AddScoped<ICommitteesAndAssociationsService, CommitteesAndAssociationsService>();
            services.AddScoped<Func<ICommitteesAndAssociationsService>>(provider =>
            () => provider.GetRequiredService<ICommitteesAndAssociationsService>()
            );

            services.AddScoped<IParticipationInMagazinesService, ParticipationInMagazinesService>();
            services.AddScoped<Func<IParticipationInMagazinesService>>(provider =>
            () => provider.GetRequiredService<IParticipationInMagazinesService>()
            );

            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddScoped<Func<IProjectsService>>(provider =>
            () => provider.GetRequiredService<IProjectsService>()
            );

            services.AddScoped<IReviewingArticlesService, ReviewingArticlesService>();
            services.AddScoped<Func<IReviewingArticlesService>>(provider =>
            () => provider.GetRequiredService<IReviewingArticlesService>()
            );


            services.AddScoped<IAcademicQualificationsService, AcademicQualificationsService>();
            services.AddScoped<Func<IAcademicQualificationsService>>(provider =>
            () => provider.GetRequiredService<IAcademicQualificationsService>()
            );

            services.AddScoped<IAdministrativePositionsService, AdministrativePositionsService>();
            services.AddScoped<Func<IAdministrativePositionsService>>(provider =>
            () => provider.GetRequiredService<IAdministrativePositionsService>()
            );

            services.AddScoped<IJobRanksService, JobRanksService>();
            services.AddScoped<Func<IJobRanksService>>(provider =>
            () => provider.GetRequiredService<IJobRanksService>()
            );

            services.AddScoped<IResearchesService, ResearchesService>();
            services.AddScoped<Func<IResearchesService>>(provider =>
            () => provider.GetRequiredService<IResearchesService>()
            );


            services.AddScoped<IResearcherProfileService, ResearcherProfileService>();
            services.AddScoped<Func<IResearcherProfileService>>(provider =>
            () => provider.GetRequiredService<IResearcherProfileService>()
            );


            services.AddScoped<IThesesSupervisingService, ThesesSupervisingService>();
            services.AddScoped<Func<IThesesSupervisingService>>(provider =>
            () => provider.GetRequiredService<IThesesSupervisingService>()
            );

            services.AddScoped<IThesesService, ThesesService>();
            services.AddScoped<Func<IThesesService>>(provider =>
            () => provider.GetRequiredService<IThesesService>()
            );


            services.AddScoped<IGeneralExperiencesService, GeneralExperiencesService>();
            services.AddScoped<Func<IGeneralExperiencesService>>(provider =>
            () => provider.GetRequiredService<IGeneralExperiencesService>()
            );

            services.AddScoped<ITeachingExperiencesService, TeachingExperiencesService>();
            services.AddScoped<Func<ITeachingExperiencesService>>(provider =>
            () => provider.GetRequiredService<ITeachingExperiencesService>()
            );

            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<Func<IAttachmentService>>(provider =>
            () => provider.GetRequiredService<IAttachmentService>()
            );

            services.AddScoped<IPrizesAndRewardsService, PrizesAndRewardsService>();
            services.AddScoped<Func<IPrizesAndRewardsService>>(provider =>
            () => provider.GetRequiredService<IPrizesAndRewardsService>()
            );

            services.AddScoped<IManifestationsOfScientificAppreciationService, ManifestationsOfScientificAppreciationService>();
            services.AddScoped<Func<IManifestationsOfScientificAppreciationService>>(provider =>
            () => provider.GetRequiredService<IManifestationsOfScientificAppreciationService>()
            );

            services.AddScoped<IScientificWritingsService, ScientificWritingsService>();
            services.AddScoped<Func<IScientificWritingsService>>(provider =>
            () => provider.GetRequiredService<IScientificWritingsService>()
            );

            services.AddScoped<IPatentsService, PatentsService>();
            services.AddScoped<Func<IPatentsService>>(provider =>
            () => provider.GetRequiredService<IPatentsService>()
            );

            services.AddScoped<IContributionsToCommunityServiceService, ContributionsToCommunityServiceService>();
            services.AddScoped<Func<IContributionsToCommunityServiceService>>(provider =>
            () => provider.GetRequiredService<IContributionsToCommunityServiceService>()
            );

            services.AddScoped<IContributionsToUniversityService, ContributionsToUniversityService>();
            services.AddScoped<Func<IContributionsToUniversityService>>(provider =>
            () => provider.GetRequiredService<IContributionsToUniversityService>()
            );

            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<Func<IUserManagementService>>(provider =>
            () => provider.GetRequiredService<IUserManagementService>()
            );


            services.AddScoped<IParticipationInQualityWorksService, ParticipationInQualityWorksService>();
            services.AddScoped<Func<IParticipationInQualityWorksService>>(provider =>
            () => provider.GetRequiredService<IParticipationInQualityWorksService>()
            );

            services.AddScoped<IProfileDashboardService, ProfileDashboardService>();
            services.AddScoped<Func<IProfileDashboardService>>(provider =>
            () => provider.GetRequiredService<IProfileDashboardService>()
            );

            services.AddScoped<IFacultyMemberMainDataManagementService, FacultyMemberMainDataManagementService>();
            services.AddScoped<Func<IFacultyMemberMainDataManagementService>>(provider =>
            () => provider.GetRequiredService<IFacultyMemberMainDataManagementService>()
            );



            services.AddScoped<IContributionsToCommunityServiceManagementService, ContributionsToCommunityServiceManagementService>();
            services.AddScoped<Func<IContributionsToCommunityServiceManagementService>>(provider =>
            () => provider.GetRequiredService<IContributionsToCommunityServiceManagementService>()
            );



            services.AddScoped<IFacultyMemberContributionsToUniversityManagementService, FacultyMemberContributionsToUniversityManagementService>();
            services.AddScoped<Func<IFacultyMemberContributionsToUniversityManagementService>>(provider =>
            () => provider.GetRequiredService<IFacultyMemberContributionsToUniversityManagementService>()
            );


            services.AddScoped<IFacultyMemberParticipationInQualityWorksManagementService, FacultyMemberParticipationInQualityWorksManagementService>();
            services.AddScoped<Func<IFacultyMemberParticipationInQualityWorksManagementService>>(provider =>
            () => provider.GetRequiredService<IFacultyMemberParticipationInQualityWorksManagementService>()
            );

            services.AddScoped<IFacultyMemberGeneralExperiencesManagementService, FacultyMemberGeneralExperiencesManagementService>();
            services.AddScoped<Func<IFacultyMemberGeneralExperiencesManagementService>>(provider =>
            () => provider.GetRequiredService<IFacultyMemberGeneralExperiencesManagementService>()
            );


            services.AddScoped<IFacultyMemberTeachingExperiencesManagementService, FacultyMemberTeachingExperiencesManagementService>();
            services.AddScoped<Func<IFacultyMemberTeachingExperiencesManagementService>>(provider =>
            () => provider.GetRequiredService<IFacultyMemberTeachingExperiencesManagementService>()
            );

            services.AddScoped<IFacultyMemberScientificMissionsManagementService, FacultyMemberScientificMissionsManagementService>();
            services.AddScoped<Func<IFacultyMemberScientificMissionsManagementService>>(provider =>
            () => provider.GetRequiredService<IFacultyMemberScientificMissionsManagementService>()
            );


            services.AddScoped<IFacultyMemberSeminarsAndConferencesManagementService, FacultyMemberSeminarsAndConferencesManagementService>();
            services.AddScoped<Func<IFacultyMemberSeminarsAndConferencesManagementService>>(provider =>
            () => provider.GetRequiredService<IFacultyMemberSeminarsAndConferencesManagementService>()
            );


            services.AddScoped<IFacultyMemberTrainingProgramsManagementService, FacultyMemberTrainingProgramsManagementService>();
            services.AddScoped<Func<IFacultyMemberTrainingProgramsManagementService>>(provider =>
            () => provider.GetRequiredService<IFacultyMemberTrainingProgramsManagementService>()
            );


            services.AddScoped<IFacultyMemberManifestationsOfScientificAppreciationManagementService, FacultyMemberManifestationsOfScientificAppreciationManagementService>();
            services.AddScoped<Func<IFacultyMemberManifestationsOfScientificAppreciationManagementService>>(provider =>
            () => provider.GetRequiredService<IFacultyMemberManifestationsOfScientificAppreciationManagementService>()
            );


            services.AddScoped<IFacultyMemberPrizesAndRewardsManagementService, FacultyMemberPrizesAndRewardsManagementService>();
            services.AddScoped<Func<IFacultyMemberPrizesAndRewardsManagementService>>(provider =>
            () => provider.GetRequiredService<IFacultyMemberPrizesAndRewardsManagementService>()
            );



            services.AddScoped<IFacultyMemberCommitteesAndAssociationsManagementService, FacultyMemberCommitteesAndAssociationsManagementService>();
            services.AddScoped<Func<IFacultyMemberCommitteesAndAssociationsManagementService>>(provider =>
            () => provider.GetRequiredService<IFacultyMemberCommitteesAndAssociationsManagementService>()
            );


            services.AddScoped<IFacultyMemberParticipationInMagazinesManagementService, FacultyMemberParticipationInMagazinesManagementService>();
            services.AddScoped<Func<IFacultyMemberParticipationInMagazinesManagementService>>(provider =>
            () => provider.GetRequiredService<IFacultyMemberParticipationInMagazinesManagementService>()
            );


            services.AddScoped<IFacultyMemberProjectsManagementService, FacultyMemberProjectsManagementService>();
            services.AddScoped<Func<IFacultyMemberProjectsManagementService>>(provider =>
            () => provider.GetRequiredService<IFacultyMemberProjectsManagementService>()
            );


            services.AddScoped<IFacultyMemberReviewingArticlesManagementService, FacultyMemberReviewingArticlesManagementService>();
            services.AddScoped<Func<IFacultyMemberReviewingArticlesManagementService>>(provider =>
            () => provider.GetRequiredService<IFacultyMemberReviewingArticlesManagementService>()
            );


            services.AddScoped<IFacultyMemberAcademicQualificationsManagementService, FacultyMemberAcademicQualificationsManagementService>();
            services.AddScoped<Func<IFacultyMemberAcademicQualificationsManagementService>>(provider =>
            () => provider.GetRequiredService<IFacultyMemberAcademicQualificationsManagementService>()
            );


            services.AddScoped<IFacutlyMemberAdministrativePositionsManagementService, FacutlyMemberAdministrativePositionsManagementService>();
            services.AddScoped<Func<IFacutlyMemberAdministrativePositionsManagementService>>(provider =>
            () => provider.GetRequiredService<IFacutlyMemberAdministrativePositionsManagementService>()
            );

            services.AddScoped<IFacultyMemberJobRanksManagementService, FacultyMemberJobRanksManagementService>();
            services.AddScoped<Func<IFacultyMemberJobRanksManagementService>>(provider =>
            () => provider.GetRequiredService<IFacultyMemberJobRanksManagementService>()
            );


            services.AddScoped<IFacultyMemberPatentsManagementService, FacultyMemberPatentsManagementService>();
            services.AddScoped<Func<IFacultyMemberPatentsManagementService>>(provider =>
            () => provider.GetRequiredService<IFacultyMemberPatentsManagementService>()
            );


            services.AddScoped<IGetDataFromExternalServiceGetFacultyMembersAndLookupsHelper, GetFacultyMembersAndLookupsHelper>();
            //services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

            services.AddScoped<IExternalDataHandlingService, ExternalDataHandlingService>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IProcessingService, ProcessingService>();

            services.AddScoped<AttachmentCore>();
            services.AddScoped<IAttachmentContextHandler, ResearchAttachmentHandler>();
            services.AddScoped<IAttachmentContextHandler, ThesisAttachmentHandler>();
            services.AddScoped<IAttachmentContextHandler, ProfilePictureAttachmentHandler>();
            services.AddScoped<IAttachmentContextHandler, PatentAttachmentHandler>();
            services.AddScoped<IAttachmentContextHandler, ManifestationOfScientificAppreciationAttachmentHandler>();
            services.AddScoped<IAttachmentContextHandler, PrizeAndAwardAttachmentHandler>();
            services.AddScoped<IAttachmentContextHandler, AcademicQualificationAttachmentHandler>();
            services.AddScoped<IAttachmentContextHandler, ConferenceOrSeminarAttachmentHandler>();
            
            
            
            services.AddScoped<IFacultyMemberDataHelper, FacultyMemberDataHelper>();
            services.AddScoped<IContributionsToCommunityServiceHelper, ContributionsToCommunityServiceHelper>();
            services.AddScoped<IContributionsToUniversityHelper, ContributionsToUniversityHelper>();
            services.AddScoped<IParticipationInQualityWorksServiceHelper, ParticipationInQualityWorksHelper>();
            services.AddScoped<IGeneralExperiencesHelper, GeneralExperiencesHelper>();
            services.AddScoped<ITeachingExperiencesHelper, TeachingExperiencesHelper>();
            services.AddScoped<IScientificMissionsHelper, ScientificMissionsHelper>();
            services.AddScoped<ISeminarsAndConferencesHelper, SeminarsAndConferencesHelper>();
            services.AddScoped<ITrainingProgramsHelper, TrainingProgramsHelper>();
            services.AddScoped<IManifestationsOfScientificAppreciationHelper, ManifestationsOfScientificAppreciationHelper>();
            services.AddScoped<IPrizesAndRewardsHelper, PrizesAndRewardsHelper>();
            services.AddScoped<ICommitteesAndAssociationsHelper, CommitteesAndAssociationsHelper>();
            services.AddScoped<IParticipationInMagazinesHelper, ParticipationInMagazinesHelper>();
            services.AddScoped<IProjectsHelper, ProjectsHelper>();
            services.AddScoped<IReviewingArticlesHelper, ReviewingArticlesHelper>();
            services.AddScoped<IAcademicQualificationsHelper, AcademicQualificationsHelper>();
            services.AddScoped<IAdministrativePositionsHelper, AdministrativePositionsHelper>();
            services.AddScoped<IJobRanksHelper, JobRanksHelper>();
            services.AddScoped<IPatentsHelper, PatentsHelper>();

            services.AddHttpClient<IRegistrationClientService, RegistrationClientService>();
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            services.Configure<FileStorageOptions>(
                    configuration.GetSection("FileStorage"));

            services.Configure<FtpsOptions>(
                      configuration.GetSection("FileStorage:Ftps")
                    );

           services
            .AddOptions<FtpsOptions>()
            .Bind(configuration.GetSection("FileStorage:Ftps"))
            .ValidateDataAnnotations()
            .Validate(o => !string.IsNullOrWhiteSpace(o.Host), "Host is required")
            .ValidateOnStart();


            return services;
        }
    }
}
