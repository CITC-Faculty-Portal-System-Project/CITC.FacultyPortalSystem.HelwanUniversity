using Domain.Contracts;
using Domain.Entities.AcademicDataModule.ResearchesModule;
using static Domain.Entities.BaseAttachmentEntity;

namespace Domain.Entities.EntitesAttachments
{
    public class ResearchAttachment : BaseAttachmentEntity
    {
        public int ResearchId { get; set; }
        public Research? Research { get; set; }
        public void SetOwnerKey(object key) => ResearchId = Convert.ToInt32(key);
    }
}
