using System.Linq;

namespace Shared.Dtos.AcademicDataModule.PrizesModule
{
    public record PrizesAndRewardsResponseDTO
    {
        public int Id { get; set; }
        public LookupItemDto Prize { get; set; } = null!;
        public string AwardingAuthority { get; set; } = string.Empty;
        public DateOnly DateReceived { get; set; }
        public string? Description { get; set; }
    }
}
