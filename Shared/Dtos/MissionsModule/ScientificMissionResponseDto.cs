namespace Shared.Dtos.MissionsModule
{
    public record ScientificMissionResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string UniversityOrFaculty { get; set; } = string.Empty;
        public string CountryOrCity { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }
}
