namespace Shared.Dtos.CVGenerationModule.Contributions
{
    public record CVContributionsToCommunityServiceDTO
    {
        public int Id { get; set; }
        public string? ContributionTitle { get; set; }
        public DateOnly? DateOfContribution { get; set; }
    }
}
