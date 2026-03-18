namespace Domain.Entities.FacultyMemberDataModule
{
    public class SocialMediaPlatforms : BaseEntity<int>
    {
        public string? LinkedIn { get; set; }
        public string? Instagram { get; set; }
        public string? PersonalWebsite { get; set; }
        public string? Facebook { get; set; }
        public string? X { get; set; }
        public string? YouTube { get; set; }

        #region Relation With FacultyMember
        public Guid FacultyMemberId { get; set; }
        #endregion

        #region Navigation Properties
        public FacultyMember? FacultyMember { get; set; }
        #endregion
    }
}
