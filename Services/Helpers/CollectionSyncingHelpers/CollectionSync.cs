
namespace Services.Helpers.CollectionSyncingHelpers
{
    public static class CollectionSync
    {
        public static void Sync<TChild, TAddDto, TUpdateDto, TDeleteDto, TKey>(
            ICollection<TChild> current,
            IEnumerable<TAddDto>? toAdd,
            IEnumerable<Patch<TKey, TUpdateDto>>? toUpdate,   
            IEnumerable<TDeleteDto>? toDelete,
            Func<TChild, TKey> childKey,
            Func<TDeleteDto, TKey> deleteKey,
            Func<TAddDto, TChild> mapAdd,
            Action<TUpdateDto, TChild> mapUpdate,
            Action<TChild>? onDelete = null,
            Action<TKey>? onUpdateNotFound = null,
            Action<TKey>? onDeleteNotFound = null
        )
            where TKey : notnull
        {
            var dict = current.ToDictionary(childKey);

            if (toDelete != null)
            {
                foreach (var d in toDelete)
                {
                    var id = deleteKey(d);
                    if (dict.TryGetValue(id, out var entity))
                    {
                        onDelete?.Invoke(entity);
                    }
                    else
                    {
                        onDeleteNotFound?.Invoke(id);
                    }
                }
            }

            if (toUpdate != null)
            {
                foreach (var p in toUpdate)
                {
                    if (dict.TryGetValue(p.Id, out var entity))
                    {
                        mapUpdate(p.Data, entity);
                    }
                    else
                    {
                        onUpdateNotFound?.Invoke(p.Id);
                    }
                }
            }

            if (toAdd != null)
            {
                foreach (var dto in toAdd)
                    current.Add(mapAdd(dto));
            }
        }


        public static async Task AddWhenFoundAsync<TSource, TFound, TEntity>(
          this ICollection<TEntity> target,
          IEnumerable<TSource>? source,
          Func<TSource, Task<TFound?>> fetchAsync,
          Func<TFound, TEntity> createEntity)
          where TFound : class
        {
            if (source is null) return;

            foreach (var item in source)
            {
                var found = await fetchAsync(item);
                if (found is not null)
                    target.Add(createEntity(found));
            }
        }


    public static async Task UpdateWhenFoundAsync<TSource, TFound, TEntity>(
    this ICollection<TEntity> target,
    IEnumerable<TSource>? source,
    Func<TSource, Task<TFound?>> fetchAsync,
    Func<TSource, TEntity?> findEntity,
    Action<TSource, TFound, TEntity> mapUpdate,
    Action<TSource>? onEntityNotFound = null,
    Action<TSource>? onFoundNotFound = null)
    where TFound : class
    where TEntity : class
        {
            if (source is null) return;

            foreach (var item in source)
            {
                var entity = findEntity(item);
                if (entity is null)
                {
                    onEntityNotFound?.Invoke(item);
                    continue;
                }

                var found = await fetchAsync(item);
                if (found is null)
                {
                    onFoundNotFound?.Invoke(item);
                    continue;
                }

                mapUpdate(item, found, entity);
            }
        }
    }
}
