using Shared.Enums.ResearchesModule;

namespace Shared.Dtos.ResearchesModule
{
    public record SupervisingThesesUpdateDTO
    {
        public ThesisType? Type { get; set; }
        public string? Title { get; set; } = string.Empty;
        public FacultyMemberRoleInSupervisingThesis? FacultyMemberRole { get; set; }
        public string? StudentName { get; set; } = string.Empty;
        public string? Specialization { get; set; }
        public Guid? GradeId { get; set; }
        public DateOnly? RegistrationDate { get; set; }
        public DateOnly? SupervisionFormationDate { get; set; }
        public DateOnly? DiscussionDate { get; set; }
        public DateOnly? GrantingDate { get; set; }
        public string? UniversityOrFaculty { get; set; }

    }
}
