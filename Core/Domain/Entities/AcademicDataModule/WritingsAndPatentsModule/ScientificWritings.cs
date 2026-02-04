namespace Domain.Entities.AcademicDataModule.WritingsAndPatents
{
    public class ScientificWritings : BaseEntity<int>
    {
        public string Title { get; set; } = string.Empty;
        public Guid AuthorRoleId { get; set; }
        public Lookup AuthorRole { get; set; } = null!;
        public string ISBN { get; set; } = string.Empty;
        public string PublishingHouse { get; set; } = string.Empty;
        public DateOnly PublishingDate { get; set; }
        public string? Description { get; set; }

        #region Relation With FacultyMember
        public Guid FacultyMemberId { get; set; }
        #endregion

        #region Navigations Properties
        public FacultyMember? FacultyMember { get; set; }
        #endregion
    }
}