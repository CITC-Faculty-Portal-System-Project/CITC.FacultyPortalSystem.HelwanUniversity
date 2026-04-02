using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.AcademicDataModule.WritingsAndPatents;

namespace Domain.Entities.EntitesAttachments
{
    public class PatentsAttachment : BaseAttachmentEntity
    {
        public int PatentId { get; set; }
        public Patents? Patent { get; set; }
        public void SetOwnerKey(object key) => PatentId = Convert.ToInt32(key);

    }
}
