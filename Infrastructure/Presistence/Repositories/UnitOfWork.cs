using Presistence.Identity;

namespace Presistence.Repositories
{
    public sealed class UnitOfWork(
      StoreDbContext _storeDb,
      IdentityStoreDbContext _identityDb) : IUnitOfWork
    {
        private readonly Dictionary<(Type entity, Type key), object> _repos = new();
        private readonly HashSet<DbContext> _touchedContexts = new();

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
         where TEntity : class
        where TKey : notnull
        {
            var cacheKey = (typeof(TEntity), typeof(TKey));

            if (_repos.TryGetValue(cacheKey, out var repo))
                return (IGenericRepository<TEntity, TKey>)repo;

            var ctx = ResolveContextFor<TEntity>();
            _touchedContexts.Add(ctx);

            var newRepo = new GenericRepository<TEntity, TKey>(ctx);
            _repos[cacheKey] = newRepo;

            return newRepo;
        }

        public async Task<int> SaveChangesAsync()
        {
            var total = 0;
            foreach (var ctx in _touchedContexts)
                total += await ctx.SaveChangesAsync();

            return total;
        }

        private DbContext ResolveContextFor<TEntity>()
        {
            var ns = typeof(TEntity).Namespace ?? "";

            if (ns.StartsWith("Domain.Entities.IdentityModule", StringComparison.Ordinal))
                return _identityDb;

            return _storeDb;
        }

    }
}
