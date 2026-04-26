using System.Linq.Expressions;

namespace Domain.Contracts
{
    public interface IAggregationSpecification<TEntity, TResult>
    {
        IQueryable<TResult> Apply(IQueryable<TEntity> query);
    }
}
