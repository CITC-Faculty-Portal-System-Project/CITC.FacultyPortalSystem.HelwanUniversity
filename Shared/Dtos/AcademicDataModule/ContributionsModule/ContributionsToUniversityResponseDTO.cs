using System.Linq;

namespace Shared.Dtos.AcademicDataModule.ContributionsModule
{
    public record ContributionsToUniversityResponseDTO
    {
        public int Id { get; set; }
        public string ContributionTitle { get; set; } = string.Empty;
        public LookupItemDto TypeOfContribution { get; set; } = null!;
        public DateOnly DateOfContribution { get; set; }
        public string? Description { get; set; }
    }
}
