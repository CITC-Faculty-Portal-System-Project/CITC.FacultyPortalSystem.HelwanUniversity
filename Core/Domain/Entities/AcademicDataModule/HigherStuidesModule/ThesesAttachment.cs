using Domain.Contracts;
using static Domain.Entities.BaseAttachmentEntity;

namespace Domain.Entities.AcademicDataModule.HigherStuidesModule
{
    public class ThesesAttachment : BaseAttachmentEntity
    {
        public int ThesisId { get; set; }
        public Thesis? Thesis { get; set; }
        public void SetOwnerKey(object key) => ThesisId = Convert.ToInt32(key);

    }
}
