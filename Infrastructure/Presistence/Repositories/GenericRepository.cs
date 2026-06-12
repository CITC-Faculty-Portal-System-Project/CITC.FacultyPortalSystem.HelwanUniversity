using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SkiaSharp;

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

            var result = AggregationSpecificationEvaluator
                .CreateQuery(query, spec);

            if (result.Provider is IAsyncQueryProvider)
                return await result.ToListAsync();

            return result.ToList();
        }

        public IQueryable<TEntity> GetQueryable(
     ISpecifications<TEntity, TKey> specifications)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            query = query.AsNoTracking();

            if (specifications.IsSplitQuery)
                query = query.AsSplitQuery();

             
            if (specifications.Criteria is not null)
                query = query.Where(specifications.Criteria);

            foreach (var include in specifications.IncludeExpressions)
                query = query.Include(include);

            foreach (var includeChain in specifications.IncludeChains)
                query = includeChain(query);

            if (specifications.OrderBy is not null)
                query = query.OrderBy(specifications.OrderBy);

            if (specifications.OrderByDescending is not null)
                query = query.OrderByDescending(specifications.OrderByDescending);

            if (specifications.isPaginated)
            {
                query = query
                    .Skip(specifications.Skip)
                    .Take(specifications.Take);
            }


            return query;
        }
        #endregion
    }
}