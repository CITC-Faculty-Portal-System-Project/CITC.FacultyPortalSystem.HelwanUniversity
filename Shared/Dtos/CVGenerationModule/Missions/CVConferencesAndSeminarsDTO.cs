namespace Shared.Dtos.CVGenerationModule.Missions
{
    public record CVConferencesAndSeminarsDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public LookupItemDto? RoleOfParticipation { get; set; }
        public string? OrganizingAuthority { get; set; }
        public string? Website { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Venue { get; set; } 
    }
}
