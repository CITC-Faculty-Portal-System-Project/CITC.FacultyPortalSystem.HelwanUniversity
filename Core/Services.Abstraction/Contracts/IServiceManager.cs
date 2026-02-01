using Services.Abstraction.Contracts.AttachmentsModule;

using Services.Abstraction.Contracts.AcademicDataModule.MissionsModule;
using Services.Abstraction.Contracts.AcademicDataModule.ProjectsAndCommitteesModule;
using Services.Abstraction.Contracts.AcademicDataModule.ScientificProgressionModule;

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

        #endregion
    }
}
