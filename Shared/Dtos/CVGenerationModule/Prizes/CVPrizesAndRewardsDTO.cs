namespace Shared.Dtos.CVGenerationModule.Prizes
{
    public record CVPrizesAndRewardsDTO
    {
        public int Id { get; set; }
        public LookupItemDto? Prize { get; set; } 
        public string? AwardingAuthority { get; set; } 
        public DateOnly? DateReceived { get; set; }
    }
}
