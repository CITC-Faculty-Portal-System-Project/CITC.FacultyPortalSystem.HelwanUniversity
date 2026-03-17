namespace Shared.Dtos.CVGenerationModule.Contributions
{
    public record CVParticipationInQualityWorkDTO
    {
        public int Id { get; set; }
        public string? ParticipationTitle { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
