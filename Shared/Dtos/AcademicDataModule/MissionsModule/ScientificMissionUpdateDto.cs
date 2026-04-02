namespace Shared.Dtos.AcademicDataModule.MissionsModule
{
    public record ScientificMissionUpdateDto
    {
        public string MissionName { get; set; } = string.Empty;
        public string? UniversityOrFaculty { get; set; } = string.Empty;
        public string CountryOrCity { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Notes { get; set; }

    }
}