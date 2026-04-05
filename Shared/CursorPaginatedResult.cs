namespace Shared
{
    public record CursorPaginatedResult<TItem, TCursor>
        where TItem : class
    {
        public IEnumerable<TItem> Items { get; set; } = [];
        public bool HasMore { get; set; }
        public TCursor? NextCursor { get; set; }
        public int? Count { get; set; }
    }

}
