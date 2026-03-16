namespace Shared.Dtos.CVGenerationModule.ProjectsAndCommittees
{
    public record CVProjectsDTO
    {
        public int Id { get; set; }
        public string? NameOfProject { get; set; }
        public LookupItemDto? TypeOfProject { get; set; } 
        public LookupItemDto? ParticipationRole { get; set; } 
        public string? FinancingAuthority { get; set; } 
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
