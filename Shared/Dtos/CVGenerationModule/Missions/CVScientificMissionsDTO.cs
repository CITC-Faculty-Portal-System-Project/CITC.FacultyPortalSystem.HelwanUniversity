namespace Shared.Dtos.CVGenerationModule.Missions
{
    public record CVScientificMissionsDTO
    {
        public int Id { get; set; }
        public string? MissionName { get; set; } 
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? UniversityOrFaculty { get; set; } 
        public string? CountryOrCity { get; set; } 
    }
}
