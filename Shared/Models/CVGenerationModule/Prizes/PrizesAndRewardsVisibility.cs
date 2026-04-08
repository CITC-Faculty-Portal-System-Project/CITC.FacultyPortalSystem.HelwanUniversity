namespace Shared.Models.CVGenerationModule.Prizes
{
    public class PrizesAndRewardsVisibility
    {
        public bool ShowPrizesAndRewards { get; set; } = true;
        public bool ShowPrizesAndRewardsForPublic { get; set; } = true;
        public bool ShowPrizeName { get; set; } = true;
        public bool ShowPrizeNameForPublic { get; set; } = true;
        public bool ShowawardingAuthority { get; set; } = true;
        public bool ShowawardingAuthorityForPublic { get; set; } = true;
        public bool ShowDateReceived { get; set; } = true;
        public bool ShowDateReceivedForPublic { get; set; } = true;
    }
}
