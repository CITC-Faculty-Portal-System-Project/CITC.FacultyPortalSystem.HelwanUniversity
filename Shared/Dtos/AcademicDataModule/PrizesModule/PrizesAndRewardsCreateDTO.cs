using System.Linq;

namespace Shared.Dtos.AcademicDataModule.PrizesModule
{
    public record PrizesAndRewardsCreateDTO
    {
        public Guid PrizeId { get; set; }
        public string AwardingAuthority { get; set; } = string.Empty;
        public DateOnly DateReceived { get; set; }
        public string? Description { get; set; }
        public Guid FacultyMemberId { get; set; }
    }
}
