using System.Linq.Expressions;

namespace Services.Specifications
{
    public abstract class AggregationSpecification<TEntity, TResult>
      : BaseSpecifications<TEntity, object>,
        IAggregationSpecification<TEntity, TResult>
      where TEntity : class
    {
        protected AggregationSpecification() : base(null) { }

        protected void SetCriteria(Expression<Func<TEntity, bool>> criteria)
            => Criteria = criteria;

        public abstract IQueryable<TResult> Apply(IQueryable<TEntity> query);
    }
}

