namespace Domain.Exceptions
{
    public sealed class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string key, params object[] args)
          : base(key, args)
        {

        }
    }
}
