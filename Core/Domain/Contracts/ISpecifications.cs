using System.Linq.Expressions;

namespace Domain.Contracts
{
    public interface ISpecifications<TEntity, TKey>
        where TEntity : class
        where TKey : notnull
    {
        Expression<Func<TEntity, bool>>? Criteria { get; }

        // Signature for property [Expression ==> Include]
        List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
        List<Func<IQueryable<TEntity>, IQueryable<TEntity>>> IncludeChains { get; }

        // OrderBy , OrderByDescending [Expression]
        Expression<Func<TEntity, object>>? OrderBy { get; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; }

        // Pagination [Skip - Take] [int]
        int Skip { get; }
        int Take { get; }
        bool isPaginated { get; }
        bool IsSplitQuery { get; }
    }
}