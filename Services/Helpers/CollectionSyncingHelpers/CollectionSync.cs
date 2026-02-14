
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

    }
}
