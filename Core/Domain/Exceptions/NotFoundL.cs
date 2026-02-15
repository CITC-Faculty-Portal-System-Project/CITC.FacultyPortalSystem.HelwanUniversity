namespace Domain.Exceptions
{
    public class NotFoundL : LocalizedException
    {
        public NotFoundL(string key, params object[] args) : base(key, args)
        {
        }
    }
}
