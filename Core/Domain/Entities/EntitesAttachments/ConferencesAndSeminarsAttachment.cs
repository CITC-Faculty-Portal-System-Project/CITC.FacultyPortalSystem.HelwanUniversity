using Domain.Entities.AcademicDataModule.MissionsModule;
using Domain.Entities.AcademicDataModule.ResearchesModule;

namespace Domain.Entities.EntitesAttachments
{
    public class ConferencesAndSeminarsAttachment : BaseAttachmentEntity
    {
        public int ConferenceOrSeminarId { get; set; }
        public ConferencesAndSeminars? ConferenceOrSeminar { get; set; }
        public void SetOwnerKey(object key) => ConferenceOrSeminarId = Convert.ToInt32(key);

    }
}
