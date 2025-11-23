using Services.Abstraction.Contracts;

namespace Services.Implementations
{
    public class ServiceManager(Func<IAuthenticationService> _authFactory
        , Func<ICacheService> _cacheFactory
        , Func<IEmailService> _emailFactory
        , Func<IFacultyMemberDataService> _facultyMemberDataFactory
        , Func<ILookUpItemService> _lookUpItemSerivce
        , Func<IMissionService> _missionService
        , Func<ISeminarsAndConfrencesService> _seminarsAndConfernces
        , Func<IScientificProgressionService> _scientificProgressionFactory
        , Func<IProjectsAndCommitteesService> _ProjectsAndCommitteesFactory) : IServiceManager
    {
        public IAuthenticationService AuthenticationService => _authFactory.Invoke();

        public ICacheService CacheService => _cacheFactory.Invoke();

        public IEmailService EmailService => _emailFactory.Invoke();

        public IFacultyMemberDataService FacultyMemberDataService => _facultyMemberDataFactory.Invoke();
        public ILookUpItemService LookUpItemService => _lookUpItemSerivce.Invoke();
        public IMissionService MissionService => _missionService.Invoke();
        public ISeminarsAndConfrencesService SeminarsAndConfrencesService => _seminarsAndConfernces.Invoke();

        public IScientificProgressionService ScientificProgressionService => _scientificProgressionFactory.Invoke();

        public IProjectsAndCommitteesService ProjectsAndCommitteesService => _ProjectsAndCommitteesFactory.Invoke();
    }
}
