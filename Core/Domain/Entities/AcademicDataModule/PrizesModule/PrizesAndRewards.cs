using Domain.Entities.EntitesAttachments;

namespace Domain.Entities.AcademicDataModule.PrizesModule
{
    public class PrizesAndRewards : BaseEntity<int>
    {
        public Guid PrizeId { get; set; }
        public Lookup Prize { get; set; } = null!;
        public string AwardingAuthority { get; set; } = string.Empty;
        public DateOnly DateReceived { get; set; }
        public string? Description { get; set; }

        #region Relation With FacultyMember
        public Guid FacultyMemberId { get; set; }
        #endregion

        #region Navigation Properties
        public FacultyMember? FacultyMember { get; set; }
        public ICollection<PrizesAndAwardsAttachment>? Attachments { get; set; } = new List<PrizesAndAwardsAttachment>();
        #endregion
    }
}