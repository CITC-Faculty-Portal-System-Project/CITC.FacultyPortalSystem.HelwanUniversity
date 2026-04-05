namespace Services.Helpers.PaginationHelpers
{
    public static class CursorPaginationHelper
    {
        public static (List<T> Items, bool HasMore, TCursor? NextCursor)
            ProcessCursorPagination<T, TCursor, TOrder>(
                List<T> items,
                int take,
                Func<T, TCursor> cursorSelector,
                Func<T, TOrder> orderSelector)
        {
            var hasMore = items.Count > take;

            if (hasMore)
                items = items.Take(take).ToList();

            var orderedItems = items.OrderBy(orderSelector).ToList();

            TCursor? nextCursor = hasMore && orderedItems.Count > 0
                ? cursorSelector(orderedItems.First())
                : default;

            return (orderedItems, hasMore, nextCursor);
        }
    }
}
