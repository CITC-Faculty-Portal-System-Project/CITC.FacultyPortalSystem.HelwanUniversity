namespace Shared.Dtos.ProjectsAndCommitteesModule
{
    public class CommitteesAndAssociationsCreateDto
    {
        public string NameOfCommitteeOrAssociation { get; set; } = string.Empty;
        public Guid TypeOfCommitteeOrAssociationId { get; set; } 
        public Guid DegreeOfSubscriptionId { get; set; } 
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Notes { get; set; }

        public Guid FacultyMemeberId { get; set; }
    }
}
