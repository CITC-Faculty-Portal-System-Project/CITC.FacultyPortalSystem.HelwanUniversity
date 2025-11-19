using Services.Abstraction.Contracts;

namespace Services.Implementations
{
    public class ServiceManager(Func<IAuthenticationService> _authFactory
        , Func<ICacheService> _cacheFactory
        , Func<IEmailService> _emailFactory) : IServiceManager
    {
        public IAuthenticationService AuthenticationService => _authFactory.Invoke();

        public ICacheService CacheService => _cacheFactory.Invoke();

        public IEmailService EmailService => _emailFactory.Invoke();

    }
}
