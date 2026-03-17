namespace Shared.Dtos.CVGenerationModule.Experiences
{
    public record CVTeachingExperienceDTO
    {
        public int Id { get; set; }
        public string? CourseName { get; set; } 
        public string? AcademicLevel { get; set; }
        public string? UniversityOrFaculty { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
