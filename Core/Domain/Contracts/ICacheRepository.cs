namespace Domain.Contracts
{
    public interface ICacheRepository
    {
        //Get ==> Already cached [return data] ==> cached response
        Task<string?> GetAsync(string key);

        //Set ==> Not cached [store data in cache]
        Task SetAsync(string key, object value, TimeSpan duration);
    }
}
