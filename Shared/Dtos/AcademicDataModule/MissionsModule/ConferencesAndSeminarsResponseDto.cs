using Shared.Dtos.AttachmentsModule;
using Shared.Enums.AcademicDataModule.MissionsModule;
using System.Linq;

namespace Shared.Dtos.AcademicDataModule.MissionsModule
{
    public record ConferencesAndSeminarsResponseDto
    {
        public int Id { get; set; }
        public ConferenceOrSeminar Type { get; set; }
        public LocalOrInternational LocalOrInternational { get; set; }
        public string Name { get; set; } = string.Empty;

        public Guid RoleOfParticipationId { get; set; }
        public string OrganizingAuthority { get; set; } = string.Empty;
        public string? Website { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string Venue { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;

        public Guid FacultyMemberId { get; set; }

        public ICollection<AttachmentResponseDTO>? Attachments { get; set; }
    }
}
