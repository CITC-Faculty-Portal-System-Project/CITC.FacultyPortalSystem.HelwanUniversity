namespace Shared.Dtos
{
    public record LookupItemDto
    {
        public Guid Id { get; set; }
        public string ValueAr { get; set; } = string.Empty;
        public string ValueEn { get; set; } = string.Empty;
    }
}
