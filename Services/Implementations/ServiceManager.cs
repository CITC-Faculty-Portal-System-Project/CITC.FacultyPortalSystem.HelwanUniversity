using Services.Abstraction.Contracts.AcademicDataModule.MissionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ResearchesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule;

using Services.Abstraction.Contracts.AttachmentsModule;
using System;

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
        /*, Func<IExternalDataHandlingService> _externalDataHandlingService*/) : IServiceManager
    {
        public IAuthenticationService AuthenticationService => _authFactory.Invoke();

        public ICacheService CacheService => _cacheFactory.Invoke();

        public IEmailService EmailService => _emailFactory.Invoke();

        public IFacultyMemberDataService FacultyMemberDataService => _facultyMemberDataFactory.Invoke();
        public ILookUpItemService LookUpItemService => _lookUpItemSerivce.Invoke();
        public IAttachmentService AttachmentService => _attachmentService.Invoke();
        //public IExternalDataHandlingService ExternalDataHandlingService => _externalDataHandlingService.Invoke();

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

        #region ResearchesModule

        public IResearchesService ResearchesService => _researchesService.Invoke();
        public IResearcherProfileService ResearcherProfileService => _researcherProfileService.Invoke();
        public IThesesSupervisingService ThesesSupervisingService => _thesesSupervisingService.Invoke();
        public IThesesService ThesesService => _thesesService.Invoke();

        #endregion

        #endregion


    }
}
