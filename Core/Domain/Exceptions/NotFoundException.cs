namespace Domain.Exceptions
{
    public class NotFoundException : LocalizedException
    {
        public NotFoundException(string key, params object[] args)
          : base(key, args)
        {
        }
    }
}
