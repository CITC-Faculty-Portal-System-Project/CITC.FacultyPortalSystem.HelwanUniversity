namespace Shared.Dtos.CVGenerationModule.ProjectsAndCommittees
{
    public record CVParticipationInMagazinesDTO
    {
        public int Id { get; set; }
        public string? NameOfMagazine { get; set; } 
        public string? WebsiteOfMagazine { get; set; }
        public LookupItemDto? TypeOfParticipation { get; set; } 
    }
}
