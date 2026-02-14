namespace Shared.Dtos.AcademicDataModule.PrizesModule
{
    public record PrizesAndRewardsUpdateDTO
    {
        public Guid PrizeId { get; set; }
        public string AwardingAuthority { get; set; } = string.Empty;
        public DateOnly DateReceived { get; set; }
        public string? Description { get; set; }
    }
}
