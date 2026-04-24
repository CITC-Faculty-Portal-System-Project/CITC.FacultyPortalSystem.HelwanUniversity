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
using Services.Abstraction.Contracts.MessagingAndChattingModule;
using Services.Abstraction.Contracts.TicketingModule;
using Services.Abstraction.Contracts.CVGenerationModule;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Abstraction.Contracts.Notification;

namespace Services.Implementations
{
    public class ServiceManager(Func<IAuthenticationService> _authFactory
        , Func<ICacheService> _cacheFactory
        , Func<IEmailService> _emailFactory
        , Func<IFacultyMemberDataService> _facultyMemberDataFactory
        , Func<ILookUpItemService> _lookUpItemSerivce
        , Func<IAttachmentService> _attachmentService
        , Func<IScientificMissionsService> _scientificMissionsServiceFactory
        , Func<ISeminarsAndConferencesService> _seminarsAndConferencesServiceFactory
        , Func<ITrainingProgramsService> _trainingProgramsServiceFactory
        , Func<ICommitteesAndAssociationsService> _committeeAndAssociationsServiceFactory
        , Func<IParticipationInMagazinesService> _participationInMagazinesServiceFactory
        , Func<IReviewingArticlesService> _reviewingArticlesServiceFactory
        , Func<IProjectsService> _projectsServiceFactory
        , Func<IAcademicQualificationsService> _academicQualificationsServiceFactory
        , Func<IAdministrativePositionsService> _administrativePositionsServiceFactory
        , Func<IJobRanksService> _jobRanksServiceFactory
        , Func<IResearchesService> _researchesService
        , Func<IResearcherProfileService> _researcherProfileService
        , Func<IThesesSupervisingService> _thesesSupervisingService
        , Func<IThesesService> _thesesService
        , Func<IGeneralExperiencesService> _generalExperiencesServiceFactory
        , Func<ITeachingExperiencesService> _teachingExperiencesServiceFactory
        , Func<IPrizesAndRewardsService> _prizesAndRewardsServiceFactory
        , Func<IManifestationsOfScientificAppreciationService> _manifestationsOfScientificAppreciationServiceFactory
        , Func<IScientificWritingsService> _scientificWritingsServiceFactory
        , Func<IPatentsService> _patentsServiceFactory
        , Func<IContributionsToCommunityServiceService> _contributionsToCommunityServiceFactory
        , Func<IContributionsToUniversityService> _contributionsToUniversityServiceFactory
        , Func<IParticipationInQualityWorksService> _participationInQualityWorksServiceFactory
        , Func<IProfileDashboardService> _profileDashboardServiceFactory
        , Func<IUserManagementService> _userManagementService
        , Func<IChatService> _chatService
        , Func<IConversationService> _conversationService
        , Func<ITicketingService> _ticketingService
        , Func<ICVGenerationService> _cvGenerationServiceFactory
        , Func<INotificationService> _notificationService
        /*, Func<IExternalDataHandlingService> _externalDataHandlingService*/) : IServiceManager
    {
        public IAuthenticationService AuthenticationService => _authFactory.Invoke();

        public ICacheService CacheService => _cacheFactory.Invoke();

        public IEmailService EmailService => _emailFactory.Invoke();

        public IFacultyMemberDataService FacultyMemberDataService => _facultyMemberDataFactory.Invoke();
        public ILookUpItemService LookUpItemService => _lookUpItemSerivce.Invoke();
        public IAttachmentService AttachmentService => _attachmentService.Invoke();

        public IProfileDashboardService ProfileDashboardService => _profileDashboardServiceFactory.Invoke();
        //public IExternalDataHandlingService ExternalDataHandlingService => _externalDataHandlingService.Invoke();

        public ICVGenerationService CVGenerationService => _cvGenerationServiceFactory.Invoke();

        #region Notification
        public INotificationService NotificationService => _notificationService.Invoke(); 
        #endregion

        #region Academic Data Module

        #region Missions Module
        public IScientificMissionsService ScientificMissionsService => _scientificMissionsServiceFactory.Invoke();
        public ISeminarsAndConferencesService SeminarsAndConferencesService => _seminarsAndConferencesServiceFactory.Invoke();
        public ITrainingProgramsService TrainingProgramsService => _trainingProgramsServiceFactory.Invoke();

        #endregion

        #region Projects And Committees Module
        public ICommitteesAndAssociationsService CommitteesAndAssociationsService => _committeeAndAssociationsServiceFactory.Invoke();
        public IReviewingArticlesService ReviewingArticlesService => _reviewingArticlesServiceFactory.Invoke();
        public IParticipationInMagazinesService ParticipationInMagazinesService => _participationInMagazinesServiceFactory.Invoke();
        public IProjectsService ProjectsService => _projectsServiceFactory.Invoke();
        #endregion

        #region Scientific Progression Module
        public IAcademicQualificationsService AcademicQualificationsService => _academicQualificationsServiceFactory.Invoke();
        public IAdministrativePositionsService AdministrativePositionsService => _administrativePositionsServiceFactory.Invoke();
        public IJobRanksService JobRanksService => _jobRanksServiceFactory.Invoke();
        #endregion

        #region Expriences Module
        public IGeneralExperiencesService GeneralExperiencesService => _generalExperiencesServiceFactory.Invoke();
        public ITeachingExperiencesService TeachingExperiencesService => _teachingExperiencesServiceFactory.Invoke();
        #endregion

        #region Prizes Module
        public IPrizesAndRewardsService PrizesAndRewardsService => _prizesAndRewardsServiceFactory.Invoke();
        public IManifestationsOfScientificAppreciationService ManifestationsOfScientificAppreciationService => _manifestationsOfScientificAppreciationServiceFactory.Invoke();
        #endregion

        #region Writings And Patents Module
        public IScientificWritingsService ScientificWritingsService => _scientificWritingsServiceFactory.Invoke();
        public IPatentsService PatentsService => _patentsServiceFactory.Invoke();
        #endregion

        #region Contributions Module
        public IContributionsToCommunityServiceService ContributionsToCommunityService => _contributionsToCommunityServiceFactory.Invoke();
        public IContributionsToUniversityService ContributionsToUniversityService => _contributionsToUniversityServiceFactory.Invoke();
        public IParticipationInQualityWorksService ParticipationInQualityWorksService => _participationInQualityWorksServiceFactory.Invoke();
        #endregion

        #region ResearchesModule

        public IResearchesService ResearchesService => _researchesService.Invoke();
        public IResearcherProfileService ResearcherProfileService => _researcherProfileService.Invoke();
        public IThesesSupervisingService ThesesSupervisingService => _thesesSupervisingService.Invoke();
        public IThesesService ThesesService => _thesesService.Invoke();

        #endregion



        #endregion

        #region AdminModule

        public IUserManagementService UserManagementService => _userManagementService.Invoke();

        #endregion

        #region MessagingAndChattingModule

        public IChatService ChatService => _chatService.Invoke();
        public IConversationService ConversationService => _conversationService.Invoke();


        #endregion

        #region TicketingModule

        public ITicketingService TicketingService => _ticketingService.Invoke();


        #endregion

    }
}
