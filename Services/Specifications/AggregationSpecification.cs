using System.Linq.Expressions;

namespace Services.Specifications
{
    public abstract class AggregationSpecification<TEntity, TResult>
        : IAggregationSpecification<TEntity, TResult>
    {
        protected Expression<Func<TEntity, bool>>? Criteria;

        public void SetCriteria(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        public abstract IQueryable<TResult> Apply(IQueryable<TEntity> query);
    }
}

