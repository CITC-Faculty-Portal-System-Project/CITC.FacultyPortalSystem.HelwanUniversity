using Domain.Entities.Attachments;

namespace Presistence.Data.Configurations.AttachmentReferenceConfigurations
{
    public class ConferencesAndSeminarsAttachmentsConfiguration : IEntityTypeConfiguration<ConferencesAndSeminarsAttachments>
    {
        public void Configure(EntityTypeBuilder<ConferencesAndSeminarsAttachments> builder)
        {
            builder.HasIndex(x => new { x.AttachmentId, x.ConferenceOrSeminarId })
                .IsUnique();

            builder.HasOne(x => x.ConferenceOrSeminar)
                   .WithMany(u => u.Attachments)
                   .HasForeignKey(x => x.ConferenceOrSeminarId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Attachment)
                   .WithMany(a => a.ConferencesOrSeminars)
                   .HasForeignKey(x => x.AttachmentId)
                   .OnDelete(DeleteBehavior.Restrict);
            

        }
    }
}
