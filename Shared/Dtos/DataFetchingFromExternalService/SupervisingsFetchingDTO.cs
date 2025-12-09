namespace Shared.Dtos.DataFetchingFromExternalService
{
    public record SupervisingsFetchingDTO
    {
        public string NationalNumber { get; set; } = string.Empty;
        public string ThesisType { get; set; } = string.Empty;
        public string ThesisTitle { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public DateOnly RegistrationDate { get; set; }
        public DateOnly? GrantingDate { get; set; }
        public DateOnly SupervisionFormationDate { get; set; }
        public DateOnly? DiscussionDate { get; set; }
        public string UniversityFaculty { get; set; } = string.Empty;
    }
}
