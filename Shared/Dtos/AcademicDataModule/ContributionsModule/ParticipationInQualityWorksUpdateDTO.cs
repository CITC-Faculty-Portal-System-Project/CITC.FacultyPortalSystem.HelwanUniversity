namespace Shared.Dtos.AcademicDataModule.ContributionsModule
{
    public record ParticipationInQualityWorksUpdateDTO
    {
        public string ParticipationTitle { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; } = string.Empty;

    }
}