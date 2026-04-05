using FtpFileStorage.Configurations;
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
using Services.Abstraction.Contracts.CVGenerationModule;
using Services.Abstraction.Contracts.AttachmentsModule.Helpers;
using Services.Abstraction.Contracts.MessagingAndChattingModule;
using Services.Abstraction.Contracts.TicketingModule;
using Services.Abstraction.EncryptionServices;
using Services.EncryptionServices;
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
using Services.Implementations.CVGenerationModule;
using Services.Implementations.CVGenerationModule.DataFilters;
using Services.Implementations.CVGenerationModule.Factories;
using Services.Implementations.CVGenerationModule.SectionFilters;
using Services.Implementations.CVGenerationModule.SectionFilters.Contributions;
using Services.Implementations.CVGenerationModule.SectionFilters.Experiences;
using Services.Implementations.CVGenerationModule.SectionFilters.Missions;
using Services.Implementations.CVGenerationModule.SectionFilters.Prizes;
using Services.Implementations.CVGenerationModule.SectionFilters.ProjectsAndCommittees;
using Services.Implementations.CVGenerationModule.SectionFilters.ScientificProgression;
using Services.Implementations.CVGenerationModule.SectionFilters.WritingsAndPatents;
using Services.Implementations.AttachmentsModule.Helpers;
using Services.Implementations.AttachmentsModule.Helpers.Handlers;
using Services.Implementations.CVGenerationModule.Templates;
using Services.Implementations.MessagingAndChattingModule;
using Services.Implementations.TicketingModule;
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


            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<Func<IChatService>>(provider =>
            () => provider.GetRequiredService<IChatService>()
            );

            services.AddScoped<IConversationService, ConversationService>();
            services.AddScoped<Func<IConversationService>>(provider =>
            () => provider.GetRequiredService<IConversationService>()
            );


            services.AddScoped<ITicketingService, TicketingService>();
            services.AddScoped<Func<ITicketingService>>(provider =>
            () => provider.GetRequiredService<ITicketingService>()
            );



            services.AddScoped<ICVGenerationService, CVGenerationService>();
            services.AddScoped<Func<ICVGenerationService>>(provider =>
            () => provider.GetRequiredService<ICVGenerationService>()
            );

            services.AddScoped<IGetDataFromExternalServiceGetFacultyMembersAndLookupsHelper, GetFacultyMembersAndLookupsHelper>();
            //services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

            services.AddScoped<IExternalDataHandlingService, ExternalDataHandlingService>();
            services.AddScoped<IAttachmentEncryptionService, AttachmentEncryptionService>();
            services.AddScoped<IMessageEncryptionService, MessageEncryptionService>();
            services.AddScoped<IProcessingService, ProcessingService>();

            services.AddScoped<ICVSectionVisibilityFilter, PersonalDataVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, ContactVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, SocialMediaVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, AcademicQualificationVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, JobRanksVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, AdministrativePositionsVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, ConferencesAndSeminarsVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, ScientificMissionsVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, TrainingProgramsVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, CommitteesAndAssociationsVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, ParticipationInMagazinesVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, ProjectsVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, ReviewingArticlesVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, GeneralExperiencesVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, TeachingExperiencesVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, ScientificWritingsVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, PatentsVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, PrizesAndRewardsVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, ManifestationsOfScientificAppreciationsVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, ParticipationInQualityWorkVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, ContributionsToCommunityServiceVisibilityFilter>();
            services.AddScoped<ICVSectionVisibilityFilter, ContributionsToUniversityVisibilityFilter>();
            services.AddScoped<AttachmentCore>();
            services.AddScoped<IAttachmentContextHandler, ResearchAttachmentHandler>();
            services.AddScoped<IAttachmentContextHandler, ThesisAttachmentHandler>();
            services.AddScoped<IAttachmentContextHandler, ProfilePictureAttachmentHandler>();
            services.AddScoped<IAttachmentContextHandler, PatentAttachmentHandler>();
            services.AddScoped<IAttachmentContextHandler, ManifestationOfScientificAppreciationAttachmentHandler>();
            services.AddScoped<IAttachmentContextHandler, PrizeAndAwardAttachmentHandler>();
            services.AddScoped<IAttachmentContextHandler, AcademicQualificationAttachmentHandler>();
            services.AddScoped<IAttachmentContextHandler, ConferenceOrSeminarAttachmentHandler>();
            services.AddScoped<ICVTemplate, ModernTemplateCV>();
            services.AddScoped<ICVTemplate, AcademicTemplateCV>();
            services.AddScoped<ICVTemplate, ProfessionalTemplateCV>();

            services.AddScoped<CVTemplatesFactory>();

            
            
            
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
