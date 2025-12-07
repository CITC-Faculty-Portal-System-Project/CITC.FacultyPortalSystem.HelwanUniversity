namespace Shared.Dtos.DataFetchingFromExternalService
{
    public record PersonalDataFetchingDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string SocialStatus { get; set; } = string.Empty;
        public DateOnly? BirthDate { get; set; }
        public string? BirthPlace { get; set; }
        public string? NameInCompositions { get; set; }
        public string? CompositionTopics { get; set; }

    }
}
