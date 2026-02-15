namespace Domain.Exceptions
{
    public sealed class UserAlreadyExistsException : LocalizedException
    {
        public UserAlreadyExistsException(string key, params object[] args)
          : base(key, args)
        {

        }
    }
}
