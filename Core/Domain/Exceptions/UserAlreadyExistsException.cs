namespace Domain.Exceptions
{
    public sealed class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string message = $"User Already Exists") : base(message)
        {
        }
    }
}
