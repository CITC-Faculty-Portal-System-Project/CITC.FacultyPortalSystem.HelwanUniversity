namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : class
        where TKey : notnull
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false);

        Task<TEntity?> GetByIdAsync(TKey id);

        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications);

        Task<TEntity?> GetAsync(ISpecifications<TEntity, TKey> specifications);

        Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications);
        Task<IReadOnlyList<TResult>> ExecuteAggregationAsync<TResult>(
       IAggregationSpecification<TEntity, TResult> spec);  
            
    }
}