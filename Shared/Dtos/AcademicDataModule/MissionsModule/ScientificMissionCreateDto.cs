using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.AcademicDataModule.MissionsModule
{
    public record ScientificMissionCreateDto
    {
        public string MissionName { get; set; } = string.Empty;
        public string? UniversityOrFaculty { get; set; } = string.Empty;
        public string CountryOrCity { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Notes { get; set; }

        public Guid FacultyMemberId { get; set; }
    }
}
