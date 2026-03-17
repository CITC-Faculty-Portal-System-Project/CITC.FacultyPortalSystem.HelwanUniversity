namespace Shared.Dtos.CVGenerationModule.Contributions
{
    public record CVContributionsToUniversityDTO
    {
        public int Id { get; set; }
        public string? ContributionTitle { get; set; } 
        public LookupItemDto? TypeOfContribution { get; set; } 
        public DateOnly? DateOfContribution { get; set; }
    }
}
