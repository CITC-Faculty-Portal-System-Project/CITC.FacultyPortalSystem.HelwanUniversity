namespace Shared.Dtos.AcademicDataModule.ContributionsModule
{
    public record ContributionsToCommunityServiceUpdateDTO
    {
        public string ContributionTitle { get; set; } = string.Empty;
        public DateOnly DateOfContribution { get; set; }
        public string? Description { get; set; }
    }
}
