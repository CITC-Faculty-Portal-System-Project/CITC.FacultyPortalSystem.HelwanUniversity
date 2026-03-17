namespace Shared.Models.CVGenerationModule.ProjectsAndCommittees
{
    public class CommitteesAndAssociationsVisibility
    {
        public bool ShowCommitteesAndAssociations { get; set; } = true;
        public bool ShowNameOfCommitteeOrAssociation { get; set; } = true;
        public bool ShowTypeOfCommitteeOrAssociation { get; set; } = true;
        public bool ShowDegreeOfSubscription { get; set; } = true;
        public bool ShowCommitteesAndAssociationsStartDate { get; set; } = true;
        public bool ShowCommitteesAndAssociationsEndDate { get; set; } = true;
    }
}
