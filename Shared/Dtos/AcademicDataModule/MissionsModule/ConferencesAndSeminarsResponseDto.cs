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
        public LookupItemDto RoleOfParticipation { get; set; } = null!;
        public string OrganizingAuthority { get; set; } = string.Empty;
        public string? Website { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string Venue { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;

        public ICollection<ConferencesAndSeminarsAttachmentsReadDTO>? Attachments { get; set; }
    }
}
