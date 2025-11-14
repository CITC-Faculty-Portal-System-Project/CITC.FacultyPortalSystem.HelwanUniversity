namespace Domain.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message = $"Invalid Email Or Password") : base(message)
        {
        }
    }
}
