namespace Domain.Exceptions
{
    public abstract class LocalizedException : Exception
    {
        public string Key { get; }
        public object[] Args { get; }

        protected LocalizedException(string key, params object[] args)
            : base(key) 
        {
            Key = key;
            Args = args ?? Array.Empty<object>();
        }
    }

}
