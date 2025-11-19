namespace Domain.Exceptions
{
    public sealed class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message = $"Invalid Username Or Password") : base(message)
        {
        }
    }
}
