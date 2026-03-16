namespace Shared.Dtos.CVGenerationModule.WritingsAndPatents
{
    public record CVScientificWritingDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; } 
        public LookupItemDto? AuthorRole { get; set; } 
        public string? ISBN { get; set; } 
        public string? PublishingHouse { get; set; } 
        public DateOnly? PublishingDate { get; set; }
    }
}
