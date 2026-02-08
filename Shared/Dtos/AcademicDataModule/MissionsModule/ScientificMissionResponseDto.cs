namespace Shared.Dtos.AcademicDataModule.MissionsModule
{
    public record ScientificMissionResponseDto
    {
        public int Id { get; set; }
        public string MissionName { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string UniversityOrFaculty { get; set; } = string.Empty;
        public string CountryOrCity { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

    }
}
