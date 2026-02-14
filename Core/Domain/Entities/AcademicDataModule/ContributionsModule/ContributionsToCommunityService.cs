namespace Domain.Entities.AcademicDataModule.ContributionsModule
{
    public class ContributionsToCommunityService : BaseEntity<int>
    {
        public string ContributionTitle { get; set; } = string.Empty;
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
