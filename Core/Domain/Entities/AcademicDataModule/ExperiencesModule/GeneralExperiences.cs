namespace Domain.Entities.AcademicDataModule.ExperiencesModule
{
    public class GeneralExperiences : BaseEntity<int>
    {
        public string ExperienceTitle { get; set; } = string.Empty;
        public string Authority { get; set; } = string.Empty;
        public string CountryOrCity { get; set; } = string.Empty;
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
