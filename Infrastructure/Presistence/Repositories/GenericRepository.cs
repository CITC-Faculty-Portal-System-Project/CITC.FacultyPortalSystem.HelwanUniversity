using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(DbContext _dbContext)
       : IGenericRepository<TEntity, TKey>
        where TEntity : class
        where TKey : notnull
    {

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false)
            => asNoTracking
                ? await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync()
                : await _dbContext.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(TKey id)
            => await _dbContext.Set<TEntity>().FindAsync(id);

        public async Task AddAsync(TEntity entity)
            => await _dbContext.Set<TEntity>().AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
            => await _dbContext.Set<TEntity>().AddRangeAsync(entities);

        public void Update(TEntity entity)
            => _dbContext.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);

        #region Specifications

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
            => await SpecificationEvaluator
                .CreateQuery(_dbContext.Set<TEntity>(), specifications)
                .ToListAsync();

        public async Task<TEntity?> GetAsync(ISpecifications<TEntity, TKey> specifications)
            => await SpecificationEvaluator
                .CreateQuery(_dbContext.Set<TEntity>(), specifications)
                .FirstOrDefaultAsync();

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
            => await SpecificationEvaluator
                .CreateQuery(_dbContext.Set<TEntity>(), specifications)
                .CountAsync();

        public async Task<IReadOnlyList<TResult>> ExecuteAggregationAsync<TResult>(
            IAggregationSpecification<TEntity, TResult> spec)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();
            var result = spec.Apply(query);

            if (result is IAsyncEnumerable<TResult>)
                return await result.ToListAsync();
            else
                return result.ToList();
        }
        #endregion
    }
}