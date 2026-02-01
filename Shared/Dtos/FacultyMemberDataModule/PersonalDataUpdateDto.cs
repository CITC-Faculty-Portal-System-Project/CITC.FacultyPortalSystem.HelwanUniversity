namespace Shared.Dtos.FacultyMemberDataModule
{
    public record PersonalDataUpdateDto
    {
        public string Name { get; set; } = string.Empty;

        public Guid? TitleId { get; set; }
        public Guid? MaritalStatusId { get; set; }
        public Guid? UniversityId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? AuthorityId { get; set; }
        public Guid? FieldId { get; set; }

        public DateOnly? BirthDate { get; set; }
        public string? BirthPlace { get; set; }
        public string? GeneralSpecialization { get; set; }
        public string? AccurateSpecialization { get; set; }
        public string? NameInComposition { get; set; }
        public string? CompositionTopics { get; set; }

        public Guid? ProfilePictureId { get; set; }
    }
}
