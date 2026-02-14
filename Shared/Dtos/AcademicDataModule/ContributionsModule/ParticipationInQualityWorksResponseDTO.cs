namespace Shared.Dtos.AcademicDataModule.ContributionsModule
{
    public record ParticipationInQualityWorksResponseDTO
    {
        public int Id { get; set; }
        public string ParticipationTitle { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; } = string.Empty;
    }
}
