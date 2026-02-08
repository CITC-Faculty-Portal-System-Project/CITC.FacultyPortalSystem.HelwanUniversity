using System.Linq;

namespace Shared.Dtos.AcademicDataModule.ContributionsModule
{
    public record ContributionsToUniversityUpdateDTO
    {
        public string ContributionTitle { get; set; } = string.Empty;
        public Guid TypeOfContributionId { get; set; }
        public DateOnly DateOfContribution { get; set; }
        public string? Description { get; set; }
    }
}
