namespace Shared.Dtos.ResearchesModule
{
    public record ResearchCardResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string JournalOrConfernce { get; set; } = string.Empty;
        public string PubYear { get; set; } = string.Empty;

    }
}
