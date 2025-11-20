using Domain.Entities.FacultyMemberDataModule;

namespace Domain.Entities.ProjectsAndCommitteesModule
{
    public class CommitteesAndAssociations : BaseEntity<int>
    {
        public string NameOfCommitteeOrAssociation { get; set; } = string.Empty;

        public Guid TypeOfCommitteeOrAssociationId { get; set; }
        public Lookup TypeOfCommitteeOrAssociation { get; set; } = null!;

        public Guid DegreeOfSubscriptionId { get; set; }
        public Lookup DegreeOfSubscription { get; set; } = null!;

        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Notes { get; set; }

        #region Relation With FacultyMember
        public Guid FacultyMemberId { get; set; }
        #endregion

        #region Navigation Properties
        public FacultyMember? FacultyMember { get; set; }
        #endregion
    }
}
