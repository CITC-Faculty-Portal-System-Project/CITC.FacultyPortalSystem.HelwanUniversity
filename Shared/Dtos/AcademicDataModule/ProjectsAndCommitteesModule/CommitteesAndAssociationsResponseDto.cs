using System.Linq;

namespace Shared.Dtos.AcademicDataModule.ProjectsAndCommitteesModule
{
    public record CommitteesAndAssociationsResponseDto
    {
        public int Id { get; set; }
        public string NameOfCommitteeOrAssociation { get; set; } = string.Empty;
        public LookupItemDto TypeOfCommitteeOrAssociation { get; set; } = null!;
        public LookupItemDto DegreeOfSubscription { get; set; } = null!;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Notes { get; set; }
    }
}
