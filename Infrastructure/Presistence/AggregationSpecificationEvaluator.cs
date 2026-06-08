namespace Presistence
{
    internal static class AggregationSpecificationEvaluator
    {
        public static IQueryable<TResult> CreateQuery<TEntity, TResult>(
            IQueryable<TEntity> inputQuery,
            IAggregationSpecification<TEntity, TResult> spec)
            where TEntity : class
        {
            IQueryable<TEntity> query = inputQuery;

            var baseSpec = (ISpecifications<TEntity, object>)spec;

            if (baseSpec.Criteria is not null)
                query = query.Where(baseSpec.Criteria);

            if (baseSpec.OrderBy is not null)
                query = query.OrderBy(baseSpec.OrderBy);

            if (baseSpec.OrderByDescending is not null)
                query = query.OrderByDescending(baseSpec.OrderByDescending);

            if (baseSpec.IncludeExpressions is not null)
                query = baseSpec.IncludeExpressions
                    .Aggregate(query, (current, include) => current.Include(include));

            if (baseSpec.IncludeChains?.Count > 0)
                query = baseSpec.IncludeChains
                    .Aggregate(query, (current, include) => include(current));

            if (baseSpec.isPaginated)
                query = query.Skip(baseSpec.Skip).Take(baseSpec.Take);

            return spec.Apply(query);
        }
    }
}
