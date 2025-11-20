using Domain.Entities.FacultyMemberDataModule;

namespace Domain.Entities.ProjectsAndCommitteesModule
{
    public class Projects : BaseEntity<int>
    {
        public LocalOrInternational LocalOrInternational { get; set; }
        public string NameOfProject { get; set; } = string.Empty;

        public Guid TypeOfProjectId { get; set; }
        public Lookup TypeOfProject { get; set; } = null!;

        public Guid ParticipationRoleId { get; set; }
        public Lookup ParticipationRole { get; set; } = null!;

        public string FinancingAuthority { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; }

        #region Relation With FacultyMember
        public Guid FacultyMemberId { get; set; }
        #endregion

        #region Navigation Properties
        public FacultyMember? FacultyMember { get; set; }
        #endregion
    }
}
