
using Domain.Entities.Attachments;

namespace Presistence.Data.Configurations.AttachmentReferenceConfigurations
{
    public class FacultyMemberAttachmentsConfiguration : IEntityTypeConfiguration<FacultyMemberAttachments>
    {
        public void Configure(EntityTypeBuilder<FacultyMemberAttachments> builder)
        {
            builder.HasIndex(x => new { x.FacultyMemberId, x.AttachmentId })
                .IsUnique();

            builder.HasOne(x => x.FacultyMember)
                   .WithMany(u => u.Attachments)
                   .HasForeignKey(x => x.FacultyMemberId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.AttachmentReference)
                   .WithMany(a => a.FacultyMembers)
                   .HasForeignKey(x => x.AttachmentId)
                   .OnDelete(DeleteBehavior.Restrict);        

        }
    }
}
