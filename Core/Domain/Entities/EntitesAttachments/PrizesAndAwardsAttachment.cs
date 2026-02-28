using Domain.Entities.AcademicDataModule.PrizesModule;
using Domain.Entities.AcademicDataModule.ResearchesModule;

namespace Domain.Entities.EntitesAttachments
{
    public class PrizesAndAwardsAttachment : BaseAttachmentEntity
    {
        public int PrizeAndAwardId { get; set; }
        public PrizesAndRewards? PrizeAndAward { get; set; }

        public void SetOwnerKey(object key) => PrizeAndAwardId = Convert.ToInt32(key);

    }
}
