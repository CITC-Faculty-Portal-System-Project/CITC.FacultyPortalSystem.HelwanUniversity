namespace Services.Abstraction.Contracts
{
    public interface ICacheService
    {
        //Get
        Task<string?> GetCachedValueAsync(string key);

        //Set
        Task SetCachedValueAsync(string key, object value, TimeSpan duration);
    }
}
