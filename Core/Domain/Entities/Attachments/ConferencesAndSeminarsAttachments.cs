using Domain.Entities.AcademicDataModule.MissionsModule;

namespace Domain.Entities.Attachments
{
    public class ConferencesAndSeminarsAttachments : BaseEntity<int>
    {
        public int ConferenceOrSeminarId { get; set; }
        public ConferencesAndSeminars? ConferenceOrSeminar { get; set; }

        public Guid AttachmentId { get; set; }
        public AttachmentReference? Attachment { get; set; }
    }
}
