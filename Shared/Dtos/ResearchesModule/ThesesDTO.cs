using Microsoft.AspNetCore.Http;
using Shared.Enums.ResearchesModule;

namespace Shared.Dtos.ResearchesModule
{
    public record ThesesDTO
    {
        public ThesisType Type { get; set; }
        public string? Link { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public Guid GradeId { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public DateOnly? InternalGradeDate { get; set; }
        public DateOnly? SupervisionConfirmationDate { get; set; }
        public string? UniversityOrFaculty { get; set; }

        public DateOnly? DiscussionDate { get; set; }
        public Guid? FacultyMemberId { get; set; }
        public List<ThesesSupervisorDTO>? ComitteeMembers { get; set; }
        public List<ResearchResponseDTO>? Researches { get; set; }
    }
}
