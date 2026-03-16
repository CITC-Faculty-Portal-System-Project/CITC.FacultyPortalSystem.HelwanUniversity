namespace Shared.Dtos.CVGenerationModule.ScientificProgression
{
    public record CVJobRanksDTO
    {
        public int Id { get; set; }
        public LookupItemDto? JobRank { get; set; } 
        public DateOnly? DateOfJobRank { get; set; }
    }
}
