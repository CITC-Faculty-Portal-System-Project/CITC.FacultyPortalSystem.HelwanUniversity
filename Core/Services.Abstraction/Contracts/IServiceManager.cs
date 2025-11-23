namespace Services.Abstraction.Contracts
{
    public interface IServiceManager
    {
        public IAuthenticationService AuthenticationService { get; }
        public ICacheService CacheService { get; }
        public IEmailService EmailService { get; }
        public IFacultyMemberDataService FacultyMemberDataService { get; }
        public ILookUpItemService LookUpItemService { get; }
        public IMissionService MissionService { get; }
        public ISeminarsAndConfrencesService SeminarsAndConfrencesService { get; }
    }
}
