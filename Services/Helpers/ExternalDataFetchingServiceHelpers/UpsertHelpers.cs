namespace Services.Helpers.ExternalDataFetchingServiceHelpers
{
    public static class UpsertHelpers
    {
        public static ICollection<T> EnsureList<T>(this ICollection<T>? list)
    => list ?? new List<T>();

        public static void SetIfNotNull<T>(ref T target, T? incoming) where T : class
        {
            if (incoming is not null) target = incoming;
        }

        public static void SetIfNotNull(ref string target, string? incoming)
        {
            if (!string.IsNullOrWhiteSpace(incoming)) target = incoming;
        }

        public static TChild UpsertChild<TChild>(
            this IList<TChild> children,
            Func<TChild, bool> match,
            Func<TChild> create,
            Action<TChild>? update = null)
        {
            var existing = children.FirstOrDefault(match);
            if (existing is null)
            {
                var created = create();
                children.Add(created);
                return created;
            }

            update?.Invoke(existing);
            return existing;
        }

        public static void UpsertMany<TDto, TChild>(
            this ICollection<TChild> children,
            IEnumerable<TDto> dtos,
            Func<TDto, TChild, bool> match,
            Func<TDto, TChild> createAction,
            Action<TDto, TChild>? updateAction = null)
        {
            foreach (var dto in dtos)
            {
                var existing = children.FirstOrDefault(c => match(dto, c));
                if (existing is null)
                {
                    children.Add(createAction(dto));
                }
                else
                {
                    updateAction?.Invoke(dto, existing);
                }
            }
        }

        public static async Task<TEntity> GetOrCreateAsync<TEntity>(
            Func<Task<TEntity?>> getter,
            Func<TEntity> factory)
            where TEntity : class
        {
            return await getter() ?? factory();
        }

        public static string NormalizeName(string? s)
            => (s ?? "").Replace(".", "").Trim();
    }
}
