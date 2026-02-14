using Domain.Contracts;
using static Domain.Entities.BaseAttachmentEntity;

namespace Domain.Entities.AcademicDataModule.ResearchesModule
{
    public class ResearchAttachment : BaseAttachmentEntity
    {
        public int ResearchId { get; set; }
        public Research? Research { get; set; }
        public void SetOwnerKey(object key) => ResearchId = Convert.ToInt32(key);
    }
}
