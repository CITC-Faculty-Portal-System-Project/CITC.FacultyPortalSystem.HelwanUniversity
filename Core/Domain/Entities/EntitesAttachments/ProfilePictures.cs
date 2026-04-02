using Domain.Entities.AcademicDataModule.ResearchesModule;

namespace Domain.Entities.EntitesAttachments
{
    public class ProfilePictures : BaseAttachmentEntity
    {
        public int PersonalDataId { get; set; }
        public PersonalData? PersonalData { get; set; }

        public void SetOwnerKey(object key) => PersonalDataId = Convert.ToInt32(key);

    }
}
