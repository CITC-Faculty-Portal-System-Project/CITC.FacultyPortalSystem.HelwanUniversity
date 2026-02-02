using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Domain.Entities.Attachments;

namespace Presistence.Data.Configurations.AttachmentReferenceConfigurations
{
    public class AttachmentReferenceConfiguration : IEntityTypeConfiguration<AttachmentReference>
    {
        public void Configure(EntityTypeBuilder<AttachmentReference> builder)
        {
            #region Configuring RelationShips

            builder.HasMany(ar => ar.ConferencesOrSeminars)
                    .WithOne(cs => cs.Attachment)
                    .HasForeignKey(cs => cs.AttachmentId)
                    .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(f => f.FacultyMemberPersonalData)
              .WithOne(pp => pp.ProfilePicture)
              .HasForeignKey<PersonalData>(f => f.ProfilePictureId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.AcademicQualification)
                    .WithOne(aq => aq.Attachment)
                    .HasForeignKey<AcademicQualifications>(f => f.AttachmentId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(f => f.FacultyMembers)
                .WithOne(a => a.AttachmentReference)
                .HasForeignKey(a => a.AttachmentId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}
