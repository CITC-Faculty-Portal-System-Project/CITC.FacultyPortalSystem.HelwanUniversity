using Shared.Enums.ResearchesModule;

namespace Shared.Dtos.ResearchesModule
{
    public record SupervisingThsesResponseDTO
    {
        public int Id { get; set; }
        public ThesisType Type { get; set; }
        public string Title { get; set; } = string.Empty;
        public FacultyMemberRoleInSupervisingThesis FacultyMemberRole { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string? Specialization { get; set; }
        public LookupItemDto? Grade { get; set; } 
        public DateOnly? RegistrationDate { get; set; }
        public DateOnly? SupervisionFormationDate { get; set; }
        public DateOnly? DiscussionDate { get; set; }
        public DateOnly? GrantingDate { get; set; }
        public string? UniversityOrFaculty { get; set; }

    }
}
