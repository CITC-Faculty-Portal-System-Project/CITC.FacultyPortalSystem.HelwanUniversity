using Domain.Contracts;
using FtpFileStorage.Configurations;
using Presistence.Repositories;
using Services.Abstraction.Contracts.AcademicDataModule.ContributionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.ExperiencesModule;
using Services.Abstraction.Contracts.AcademicDataModule.MissionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.PrizesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule;
using Services.Abstraction.Contracts.AcademicDataModule.WritingsAndPatentsModule;
using Services.Abstraction.Contracts.AttachmentsModule;
using Services.Helpers.ExternalDataFetchingServiceHelpers;
using Services.Implementations.AcademicDataModule.ContributionsModule;
using Services.Implementations.AcademicDataModule.ExperiencesModule;
using Services.Implementations.AcademicDataModule.MissionsModule;
using Services.Implementations.AcademicDataModule.PrizesModule;
using Services.Implementations.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Implementations.AcademicDataModule.ScientificProgressionModule;
using Services.Implementations.AcademicDataModule.WritingsAndPatentsModule;
using Services.Implementations.AttachmentsModule;
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

            services.AddScoped<IParticipationInQualityWorksService, ParticipationInQualityWorksService>();
            services.AddScoped<Func<IParticipationInQualityWorksService>>(provider =>
            () => provider.GetRequiredService<IParticipationInQualityWorksService>()
            );

            services.AddScoped<IAttachmentsAcsessabilityService, AttachmentsAcsessablityService>();

            services.AddScoped<IGetDataFromExternalServiceGetFacultyMembersAndLookupsHelper, GetFacultyMembersAndLookupsHelper>();
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

            services.AddScoped<IExternalDataHandlingService, ExternalDataHandlingService>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IProcessingService, ProcessingService>();


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
