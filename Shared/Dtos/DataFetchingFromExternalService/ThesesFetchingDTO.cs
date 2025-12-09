namespace Shared.Dtos.DataFetchingFromExternalService
{
    public record ThesesFetchingDTO
    {
        public string NationalNumber { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public DateOnly EnrollmentDate { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public DateOnly? InternalGradeDate { get; set; }
        public DateOnly SupervisionConfirmationDate { get; set; }

        public List<ThesesSupervisorsFetchingDTO> Supervisors { get; set; } = new();
    }
}
