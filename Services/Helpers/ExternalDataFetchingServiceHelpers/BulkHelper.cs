namespace Services.Helpers.ExternalDataFetchingServiceHelpers
{
    public static class BulkHelper
    {
       public static async Task<bool> HandleAsync<TFetchDto, TCreateDto, TEntity , TKey>(
       string? json,
       Func<TFetchDto, Task<TCreateDto>> transform,
       IMapper mapper,
       IGenericRepository<TEntity , TKey> repo,
       IUnitOfWork _unitOfWork) where TEntity : BaseEntity<TKey> where TKey : notnull
        {
            var list = JsonHelper.DeserializeListOrThrow<TFetchDto>(json);
            var itemsToAdd = new List<TCreateDto>();

            foreach (var item in list)
            {
                var createdItem = await transform(item);
                if (createdItem != null)
                    itemsToAdd.Add(createdItem);
            }

            var entities = mapper.Map<IEnumerable<TEntity>>(itemsToAdd);
            await repo.AddRangeAsync(entities);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
