namespace Services.Helpers.ExternalDataFetchingServiceHelpers
{
    public static class BulkHelper
    {
        public static async Task<bool> HandleAsync<TFetchDto, TCreateDto, TEntity, TKey>(
            string? json,
            Func<TFetchDto, Task<TCreateDto>> transform,
            IMapper mapper,
            IUnitOfWork unitOfWork)
            where TEntity : BaseEntity<TKey>
            where TKey : notnull
        {
            var list = JsonHelper.DeserializeListOrThrow<TFetchDto>(json);

            var itemsToAdd = new List<TCreateDto>();
            foreach (var item in list)
            {
                var createdItem = await transform(item);
                if (createdItem is not null)
                    itemsToAdd.Add(createdItem);
            }

            var entities = mapper.Map<IEnumerable<TEntity>>(itemsToAdd);

            var repo = unitOfWork.GetRepository<TEntity, TKey>();
            await repo.AddRangeAsync(entities);

            return await unitOfWork.SaveChangesAsync() > 0;
        }
    }
}