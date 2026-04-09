namespace Shared.Models.CVGenerationModule.ProjectsAndCommittees
{
    public class CommitteesAndAssociationsVisibility
    {
        public bool ShowCommitteesAndAssociations { get; set; } = true;
        public bool ShowCommitteesAndAssociationsForPublic { get; set; } = true;
        public bool ShowNameOfCommitteeOrAssociation { get; set; } = true;
        public bool ShowNameOfCommitteeOrAssociationForPublic { get; set; } = true;
        public bool ShowTypeOfCommitteeOrAssociation { get; set; } = true;
        public bool ShowTypeOfCommitteeOrAssociationForPublic { get; set; } = true;
        public bool ShowDegreeOfSubscription { get; set; } = true;
        public bool ShowDegreeOfSubscriptionForPublic { get; set; } = true;
        public bool ShowCommitteesAndAssociationsStartDate { get; set; } = true;
        public bool ShowCommitteesAndAssociationsStartDateForPublic { get; set; } = true;
        public bool ShowCommitteesAndAssociationsEndDate { get; set; } = true;
        public bool ShowCommitteesAndAssociationsEndDateForPublic { get; set; } = true;
    }
}
