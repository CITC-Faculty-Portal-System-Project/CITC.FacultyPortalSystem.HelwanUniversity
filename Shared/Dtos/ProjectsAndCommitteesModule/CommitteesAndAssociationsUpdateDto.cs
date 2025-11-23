namespace Shared.Dtos.ProjectsAndCommitteesModule
{
    public record CommitteesAndAssociationsUpdateDto
    {
        public string NameOfCommitteeOrAssociation { get; set; } = string.Empty;
        public Guid TypeOfCommitteeOrAssociationId { get; set; }
        public Guid DegreeOfSubscriptionId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Notes { get; set; }
    }
}
