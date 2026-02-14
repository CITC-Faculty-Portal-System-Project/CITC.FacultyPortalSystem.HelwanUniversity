namespace Domain.Entities.AcademicDataModule.ContributionsModule
{
    public class ContributionsToUniversity : BaseEntity<int>
    {
        public string ContributionTitle { get; set; } = string.Empty;
        public Guid TypeOfContributionId { get; set; }
        public Lookup TypeOfContribution { get; set; } = null!;
        public DateOnly DateOfContribution { get; set; }
        public string? Description { get; set; }

        #region Relation With FacultyMember
        public Guid FacultyMemberId { get; set; }
        #endregion

        #region Navigation Properties
        public FacultyMember? FacultyMember { get; set; }
        #endregion
    }
}
