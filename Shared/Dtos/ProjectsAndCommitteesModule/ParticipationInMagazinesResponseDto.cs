namespace Shared.Dtos.ProjectsAndCommitteesModule
{
    public record ParticipationInMagazinesResponseDto
    {
        public string NameOfMagazine { get; set; } = string.Empty;
        public string? WebsiteOfMagazine { get; set; }
        public LookupItemDto TypeOfParticipation { get; set; } = null!;
    }
}
