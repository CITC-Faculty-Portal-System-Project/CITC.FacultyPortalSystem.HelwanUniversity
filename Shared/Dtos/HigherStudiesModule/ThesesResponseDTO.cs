using Shared.Enums.HigherStudiesModule;

namespace Shared.Dtos.HigherStudiesModule
{
    public record ThesesResponseDTO
    {
        public int Id { get; set; }
        public ThesesType Type { get; set; }
        public string? Link { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public Guid GradeId { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public DateOnly? InternalGradeDate { get; set; }
        public DateOnly? SupervisionConfirmationDate { get; set; }
        public Guid FacultyMemberId { get; set; }

    }
}
