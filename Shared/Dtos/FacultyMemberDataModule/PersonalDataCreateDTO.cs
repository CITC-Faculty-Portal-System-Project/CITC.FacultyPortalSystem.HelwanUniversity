namespace Shared.Dtos.FacultyMemberDataModule
{
    public record PersonalDataCreateDTO
    {
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;

        public Guid TitleId { get; set; }
        public Guid GenderId { get; set; }
        public Guid MaritalStatusId { get; set; }
        public Guid UniversityId { get; set; }
        public int DeptId { get; set; }
        public int FacultyId { get; set; }
        public Guid AuthorityId { get; set; }
        public Guid FieldId { get; set; }

        public DateOnly? BirthDate { get; set; }
        public string? BirthPlace { get; set; }
        public string? GeneralSpecialization { get; set; }
        public string? AccurateSpecialization { get; set; }
        public string? NameInComposition { get; set; }
        public string? CompositionTopics { get; set; }
        public Guid FacultyMemberId { get; set; }
    }
}
