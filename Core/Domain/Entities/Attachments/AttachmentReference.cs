
using Domain.Entities.AcademicDataModule.ScientificProgressionModule;

namespace Domain.Entities.Attachments
{
    public class AttachmentReference : BaseEntity<Guid>
    {
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long Size { get; set; }
        public string HashAlg { get; set; } = "SHA-256";
        public string Hash { get; set; } = string.Empty;
        public byte[] Nonce { get; set; } = Array.Empty<byte>();
        public byte[] Tag { get; set; } = Array.Empty<byte>();
        public string KeyRef { get; set; } = string.Empty;
        public byte[] WrappedDek { get; set; } = Array.Empty<byte>();
        public string StorageProvider { get; set; } = string.Empty;
        public string RemotePath { get; set; } = string.Empty;

        #region Relations With Other Entites
        public AcademicQualifications? AcademicQualification { get; set; }
        public PersonalData? FacultyMemberPersonalData { get; set; }
        public ICollection<ConferencesAndSeminarsAttachments>? ConferencesOrSeminars { get; set; }
        public ICollection<FacultyMemberAttachments>? FacultyMembers { get; set; }
                = new List<FacultyMemberAttachments>(); 
        
        #endregion

    }
}
