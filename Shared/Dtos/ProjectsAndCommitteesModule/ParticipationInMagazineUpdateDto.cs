namespace Shared.Dtos.ProjectsAndCommitteesModule
{
    public record ParticipationInMagazineUpdateDto
    {
        public string NameOfMagazine { get; set; } = string.Empty;
        public string? WebsiteOfMagazine { get; set; }
        public Guid TypeOfParticipationId { get; set; }
    }
}
