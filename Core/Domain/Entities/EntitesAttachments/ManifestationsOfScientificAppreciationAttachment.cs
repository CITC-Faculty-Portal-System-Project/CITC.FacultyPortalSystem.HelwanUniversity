using Domain.Entities.AcademicDataModule.PrizesModule;
using Domain.Entities.AcademicDataModule.ResearchesModule;

namespace Domain.Entities.EntitesAttachments
{
    public class ManifestationsOfScientificAppreciationAttachment : BaseAttachmentEntity
    {
        public int ManifestationOfScientificAppreciationId { get; set; }
        public ManifestationsOfScientificAppreciation? ManifestationOfScientificAppreciation { get; set; }
        public void SetOwnerKey(object key) => ManifestationOfScientificAppreciationId = Convert.ToInt32(key);

    }
}
