using Services.Abstraction.Contracts.AttachmentsModule;

using Services.Abstraction.Contracts.AcademicDataModule.MissionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule;
using Services.Abstraction.Contracts.AcademicDataModule.ExperiencesModule;
using Services.Abstraction.Contracts.AcademicDataModule.PrizesModule;
using Services.Abstraction.Contracts.AcademicDataModule.WritingsAndPatentsModule;
using Services.Abstraction.Contracts.AcademicDataModule.ContributionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Services.Abstraction.Contracts.AdminModule;


namespace Services.Abstraction.Contracts
{
    public interface IServiceManager
    {
        public IAuthenticationService AuthenticationService { get; }
        public ICacheService CacheService { get; }
        public IEmailService EmailService { get; }
        public IFacultyMemberDataService FacultyMemberDataService { get; }
        public ILookUpItemService LookUpItemService { get; }
        public IAttachmentService AttachmentService { get; }
        public IProfileDashboardService ProfileDashboardService { get; }
        //public IExternalDataHandlingService ExternalDataHandlingService { get; }

        #region Academic Data Module

        #region Missions Module
        public IScientificMissionsService ScientificMissionsService { get; }
        public ISeminarsAndConferencesService SeminarsAndConferencesService { get; }
        public ITrainingProgramsService TrainingProgramsService { get; }
        #endregion

        #region Projects And Committees Module
        public ICommitteesAndAssociationsService CommitteesAndAssociationsService { get; }
        public IReviewingArticlesService ReviewingArticlesService { get; }
        public IParticipationInMagazinesService ParticipationInMagazinesService { get; }
        public IProjectsService ProjectsService { get; }
        #endregion

        #region Scientific Progression Module
        public IAcademicQualificationsService AcademicQualificationsService { get; }
        public IAdministrativePositionsService AdministrativePositionsService { get; }
        public IJobRanksService JobRanksService { get; }
        #endregion

        #region Experiences Module
        public IGeneralExperiencesService GeneralExperiencesService { get; }
        public ITeachingExperiencesService TeachingExperiencesService { get; }
        #endregion

        #region Prizes Module
        public IPrizesAndRewardsService PrizesAndRewardsService { get; }
        public IManifestationsOfScientificAppreciationService ManifestationsOfScientificAppreciationService { get; }
        #endregion

        #region Writings And Patents Module
        public IScientificWritingsService ScientificWritingsService { get; }
        public IPatentsService PatentsService { get; }
        #endregion

        #region Contributions Module
        public IContributionsToUniversityService ContributionsToUniversityService { get; }
        public IContributionsToCommunityServiceService ContributionsToCommunityService { get; }
        public IParticipationInQualityWorksService ParticipationInQualityWorksService { get; }
        #endregion


        #region ResearchesModule

        public IResearchesService ResearchesService { get; }
        public IResearcherProfileService ResearcherProfileService { get; }
        public IThesesSupervisingService ThesesSupervisingService { get; }
        public IThesesService ThesesService { get; }


        #endregion



        #endregion

        #region AdminModule

        public IUserManagementService UserManagementService { get; }
    
        #endregion
    }
}
