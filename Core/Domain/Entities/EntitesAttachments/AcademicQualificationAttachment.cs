using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.AcademicDataModule.ScientificProgressionModule;

namespace Domain.Entities.EntitesAttachments
{
    public class AcademicQualificationAttachment : BaseAttachmentEntity
    {
        public int QualificationId { get; set; }
        public AcademicQualifications? Qualification { get; set; }
        public void SetOwnerKey(object key) => QualificationId = Convert.ToInt32(key);

    }
}
