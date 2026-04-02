namespace Domain.Entities.AcademicDataModule.HigherStuidesModule
{
    public class Supervising : BaseEntity<int>
    {
        public ThesisType Type { get; set; }
        public string Title { get; set; } = string.Empty;
        public FacultyMemberRoleInSupervisingThesis FacultyMemberRole { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string? Specialization { get; set; }
        public Guid GradeId { get; set; }
        public Lookup? Grade { get; set; }
        public bool isConfirmed { get; set; }
        public DateOnly? RegistrationDate { get; set; }
        public DateOnly? SupervisionFormationDate { get; set; }
        public DateOnly? DiscussionDate { get; set; }
        public DateOnly? GrantingDate { get; set; }
        public string? UniversityOrFaculty { get; set; }
        public Guid FacultyMemberId { get; set; }
        public FacultyMember? FacultyMember { get; set; }
        public int? ThesisId { get; set; }
        public Thesis? Thesis { get; set; }
    }
}
