namespace Domain.Entities.Attachments
{
    public class FacultyMemberAttachments : BaseEntity<int>
    {
        public Guid AttachmentId { get; set; }
        public Guid FacultyMemberId { get; set; }

        public AttachmentReference? AttachmentReference { get; set; }
        public FacultyMember? FacultyMember { get; set; }
    }
}
