namespace Domain.Exceptions
{
    public class NotFoundException : LocalizedException
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
