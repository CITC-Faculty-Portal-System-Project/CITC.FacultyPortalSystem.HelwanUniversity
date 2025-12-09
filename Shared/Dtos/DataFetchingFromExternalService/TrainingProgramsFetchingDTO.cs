namespace Shared.Dtos.DataFetchingFromExternalService
{
    public record TrainingProgramsFetchingDTO
    {
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ProgramType { get; set; } = string.Empty;
        public string ParticipationType { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string ProgramPlace { get; set; } = string.Empty;
        public string OrganizerName { get; set; } = string.Empty;
        public string NationalNumber { get; set; } = string.Empty;

    }
}
