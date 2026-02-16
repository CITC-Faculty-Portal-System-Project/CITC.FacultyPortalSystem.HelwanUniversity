namespace Presistence
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery,
            ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey> where TKey : notnull
        {
            var query = inputQuery;
            if (specifications.Criteria is not null) //where
                query = query.Where(specifications.Criteria);

            if (specifications.OrderBy is not null)
                query = query.OrderBy(specifications.OrderBy);

            if (specifications.OrderByDescending is not null)
                query = query.OrderByDescending(specifications.OrderByDescending);

            if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count > 0)
            {
                //foreach (var expression in specifications.IncludeExpressions)
                //    query = query.Include(expression);
                query = specifications.IncludeExpressions.Aggregate(query, (currentQuery, expression) => currentQuery.Include(expression));
            }

            if (specifications.IncludeChains?.Count > 0)
            {
                query = specifications.IncludeChains
                    .Aggregate(query, (current, include) => include(current));
            }

            if (specifications.isPaginated)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }

            if (specifications.IsSplitQuery)
                query = query.AsSplitQuery();


            return query;
        }
    }
}
