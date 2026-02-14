namespace Shared.Dtos.AcademicDataModule.ExperiencesModule
{
    public record TeachingExperiencesCreateDTO
    {
        public string CourseName { get; set; } = string.Empty;
        public string? AcademicLevel { get; set; }
        public string? UniversityOrFaculty { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; }
        public Guid FacultyMemberId { get; set; }
    }
}
