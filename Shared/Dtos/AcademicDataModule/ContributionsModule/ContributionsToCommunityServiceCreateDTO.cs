namespace Shared.Dtos.AcademicDataModule.ContributionsModule
{
    public record ContributionsToCommunityServiceCreateDTO
    {
        public string ContributionTitle { get; set; } = string.Empty;
        public DateOnly DateOfContribution { get; set; }
        public string? Description { get; set; }
        public Guid FacultyMemberId { get; set; }
    }
}
