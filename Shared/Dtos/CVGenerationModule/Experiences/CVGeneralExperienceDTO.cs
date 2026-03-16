namespace Shared.Dtos.CVGenerationModule.Experiences
{
    public record CVGeneralExperienceDTO
    {
        public int Id { get; set; }
        public string? ExperienceTitle { get; set; } 
        public string? Authority { get; set; } 
        public string? CountryOrCity { get; set; } 
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
