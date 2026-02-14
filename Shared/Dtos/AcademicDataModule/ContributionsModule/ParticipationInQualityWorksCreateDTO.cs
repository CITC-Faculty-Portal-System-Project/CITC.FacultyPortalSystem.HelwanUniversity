namespace Shared.Dtos.AcademicDataModule.ContributionsModule
{
    public record ParticipationInQualityWorksCreateDTO
    {
        public string ParticipationTitle { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; } = string.Empty;
        public Guid FacultyMemberId { get; set; }
    }
}
