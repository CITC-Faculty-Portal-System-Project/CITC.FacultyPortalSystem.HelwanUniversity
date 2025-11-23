namespace Shared.Dtos.ProjectsAndCommitteesModule
{
    public record ParticipationInMagazineCreateDto
    {
        public string NameOfMagazine { get; set; } = string.Empty;
        public string? WebsiteOfMagazine { get; set; }
        public Guid TypeOfParticipationId { get; set; } 

        public Guid FacultyMemberId { get; set; }
    }
}
