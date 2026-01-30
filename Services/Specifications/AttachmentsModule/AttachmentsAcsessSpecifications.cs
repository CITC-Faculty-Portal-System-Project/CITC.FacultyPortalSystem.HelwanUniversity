using Domain.Entities.Attachments;
using Microsoft.EntityFrameworkCore;

namespace Services.Specifications.AttachmentsModule
{
    internal class AttachmentsAcsessSpecifications : BaseSpecifications<AttachmentReference, Guid>
    {
        public AttachmentsAcsessSpecifications
               (Guid facultyMemberId , Guid attachmentId) :
               base(a => a.FacultyMembers!.Any(f => f.FacultyMemberId == facultyMemberId) 
                       && !a.IsDeleted
                       && a.Id == attachmentId)
        {
            AddIncludeWithChain(a=> a.Include(a=> a.FacultyMembers!)
                                      .ThenInclude(f=> f.FacultyMember));      
        }

        public AttachmentsAcsessSpecifications
               (string fileName) :
               base(a=> a.FileName == fileName && !a.IsDeleted)
        {
            AddIncludeWithChain(a => a.Include(a => a.FacultyMembers!)
                                      .ThenInclude(f => f.FacultyMember));

        }
    }
}
