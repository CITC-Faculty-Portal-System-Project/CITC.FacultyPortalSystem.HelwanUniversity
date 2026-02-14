using Shared.Enums.AcademicDataModule.MissionsModule;
using System.ComponentModel.DataAnnotations;
namespace Shared.Dtos.AcademicDataModule.MissionsModule
{
    public record ConferencesAndSeminarsCreateDto 
    {
        [Required(ErrorMessage = "You Must Enter a Type")]
        public ConferenceOrSeminar Type { get; set; }
        [Required(ErrorMessage = "You Specify Local or International")]
        public LocalOrInternational LocalOrInternational { get; set; }
        [Required(ErrorMessage = "You Must Enter a Name")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "You Must Enter your Role Of Participation")]
        public Guid RoleOfParticipationId { get; set; }
        [Required(ErrorMessage = "You Must Enter a Organinzing Authority")]
        public string OrganizingAuthority { get; set; } = string.Empty;
        public string? Website { get; set; } = string.Empty;
        [Required(ErrorMessage = "You Must Specify a Start Date")]
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        [Required(ErrorMessage = "You Must Enter a Venue")] 
        public string Venue { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;

        public Guid FacultyMemberId { get; set; }
        public ICollection<Guid>? AttachmentsIds { get; set; }
    }
}
