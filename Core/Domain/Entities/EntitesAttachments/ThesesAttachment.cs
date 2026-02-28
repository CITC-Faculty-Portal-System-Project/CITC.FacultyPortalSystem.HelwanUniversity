using Domain.Contracts;
using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using static Domain.Entities.BaseAttachmentEntity;

namespace Domain.Entities.EntitesAttachments
{
    public class ThesesAttachment : BaseAttachmentEntity
    {
        public int ThesisId { get; set; }
        public Thesis? Thesis { get; set; }
        public void SetOwnerKey(object key) => ThesisId = Convert.ToInt32(key);

    }
}
