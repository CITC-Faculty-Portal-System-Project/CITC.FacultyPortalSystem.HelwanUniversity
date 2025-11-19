namespace Domain.Exceptions
{
    public sealed class NationalNumberNotExistException : NotFoundException
    {
        public NationalNumberNotExistException(string nationalNumber)
            : base($"User with National Number '{nationalNumber}' was not found.")
        {
        }
    }
}
