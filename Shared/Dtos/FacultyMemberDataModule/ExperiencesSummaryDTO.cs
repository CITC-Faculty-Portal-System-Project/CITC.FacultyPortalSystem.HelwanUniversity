namespace Shared.Dtos.FacultyMemberDataModule
{
    public record ExperiencesSummaryDTO
    {
        public string Title { get; set; } = string.Empty;
        public string? Organization { get; set; } 
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
