namespace Domain.Entities.AcademicDataModule.ExperiencesModule
{
    public class TeachingExperiences : BaseEntity<int>
    {
        public string CourseName { get; set; } = string.Empty;
        public string? AcademicLevel { get; set; } 
        public string? UniversityOrFaculty { get; set; } 
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
