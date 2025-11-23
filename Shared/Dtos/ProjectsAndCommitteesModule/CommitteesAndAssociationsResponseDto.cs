using System.Linq;

namespace Shared.Dtos.ProjectsAndCommitteesModule
{
    public record CommitteesAndAssociationsResponseDto
    {
        public string NameOfCommitteeOrAssociation { get; set; } = string.Empty;
        public LookupItemDto TypeOfCommitteeOrAssociation { get; set; } = null!;
        public LookupItemDto DegreeOfSubscription { get; set; } = null!;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Notes { get; set; }
    }
}
