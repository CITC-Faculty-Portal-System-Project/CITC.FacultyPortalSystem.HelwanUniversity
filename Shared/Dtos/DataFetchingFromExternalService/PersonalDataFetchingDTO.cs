namespace Shared.Dtos.DataFetchingFromExternalService
{
    public record PersonalDataFetchingDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string SocialStatus { get; set; } = string.Empty;
        public DateOnly? BirthDate { get; set; }
        public string? BirthPlace { get; set; }
        public string? NameInCompositions { get; set; }
        public string? CompositionTopics { get; set; }
        public string FacultyName { get; set; } = string.Empty;
        public string FieldOfStudy { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string? GeneralSpecialization { get; set; }
        public string? AccurateSpecialization { get; set; }
        public string NationalNumber { get; set; } = string.Empty;
        public string University { get; set; } = string.Empty;
    }
}
