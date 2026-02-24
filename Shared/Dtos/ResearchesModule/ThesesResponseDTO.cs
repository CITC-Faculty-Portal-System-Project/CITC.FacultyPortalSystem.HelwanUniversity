using Shared.Dtos.AttachmentsModule;
using Shared.Enums.ResearchesModule;

namespace Shared.Dtos.ResearchesModule
{
    public record ThesesResponseDTO
    {
        public int Id { get; set; }
        public ThesisType Type { get; set; }
        public string? Link { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public Guid GradeId { get; set; }
        public LookupItemDto? Grade { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public DateOnly? InternalGradeDate { get; set; }
        public DateOnly? SupervisionConfirmationDate { get; set; }
        public Guid FacultyMemberId { get; set; }
        public List<ThesesSupervisorResponseDTO>? ComitteeMembers { get; set; }
        public List<ResearchResponseDTO>? Researches { get; set; }
        public List<AttachmentResponseDTO>? Attachments { get; set; }

    }
}
