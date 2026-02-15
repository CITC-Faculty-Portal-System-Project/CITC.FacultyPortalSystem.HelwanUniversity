namespace Domain.Exceptions
{
    public sealed class UnauthorizedException : LocalizedException
    {
        public UnauthorizedException(string key, params object[] args)
          : base(key, args)
        {

        }
    }
}
