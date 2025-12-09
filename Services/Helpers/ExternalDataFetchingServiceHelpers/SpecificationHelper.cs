namespace Services.Helpers.ExternalDataFetchingServiceHelpers
{
    public static class SpecificationHelper
    {
        public static async Task<bool> ExistsAsync<TEntity, TKey>(
        this IGenericRepository<TEntity, TKey> repo,
        ISpecifications<TEntity , TKey> spec) where TEntity : BaseEntity<TKey> where TKey : notnull
        {
            return (await repo.GetAllAsync(spec)).Any();
        }
    }
}
