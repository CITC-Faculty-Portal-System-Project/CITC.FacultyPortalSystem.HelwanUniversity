using System.Linq;

namespace Shared.Dtos.AcademicDataModule.ContributionsModule
{
    public record ContributionsToUniversityCreateDTO
    {
        public string ContributionTitle { get; set; } = string.Empty;
        public Guid TypeOfContributionId { get; set; }
        public DateOnly DateOfContribution { get; set; }
        public string? Description { get; set; }
        public Guid FacultyMemberId { get; set; }
    }
}
