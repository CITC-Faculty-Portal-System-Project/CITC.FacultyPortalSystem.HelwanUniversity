namespace Shared.Dtos
{
    public record LookupItemDto
    {
        public Guid Id { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}
