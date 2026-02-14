namespace Domain.Entities.AcademicDataModule.WritingsAndPatents
{
    public class Patents : BaseEntity<int>
    {
        public LocalOrInternational LocalOrInternational { get; set; }
        public string NameOfPatent { get; set; } = string.Empty;
        public string AccreditingAuthorityOrCountry { get; set; } = string.Empty;   
        public DateOnly ApplyingDate { get; set; }
        public DateOnly? AccreditationDate { get; set; }
        public string? Description { get; set; }

        #region Relation With FacultyMember
        public Guid FacultyMemberId { get; set; }
        #endregion

        #region Navigations Properties
        public FacultyMember? FacultyMember { get; set; }
        #endregion
    }
}