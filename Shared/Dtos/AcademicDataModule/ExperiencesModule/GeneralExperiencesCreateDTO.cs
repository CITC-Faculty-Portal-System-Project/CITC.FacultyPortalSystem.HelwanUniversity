namespace Shared.Dtos.AcademicDataModule.ExperiencesModule
{
    public record GeneralExperiencesCreateDTO
    {
        public string ExperienceTitle { get; set; } = string.Empty;
        public string Authority { get; set; } = string.Empty;
        public string CountryOrCity { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; }
        public Guid FacultyMemberId { get; set; }
    }
}
