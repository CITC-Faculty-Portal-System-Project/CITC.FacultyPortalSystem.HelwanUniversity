using Domain.Entities.Attachments;

namespace Services.Specifications.AttachmentsModule
{
    internal class AttachmentGetByIdSpecification : BaseSpecifications<AttachmentReference, Guid>
    {
        public AttachmentGetByIdSpecification
                (Guid Id) : base(a => a.Id == Id && !a.IsDeleted)
        {
        }
    }
}
