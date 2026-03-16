using Domain.Entities.AcademicDataModule.PrizesModule;

namespace Presistence.Data.Configurations.PrizesModuleConfigurations
{
    public class ManifestationsOfScientificAppreciationConfigurations : IEntityTypeConfiguration<ManifestationsOfScientificAppreciation>
    {
        public void Configure(EntityTypeBuilder<ManifestationsOfScientificAppreciation> builder)
        {
            builder.ToTable("ManifestationsOfScientificAppreciation");

            builder.HasKey(msa => msa.Id);

            builder.Property(msa => msa.TitleOfAppreciation)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(msa => msa.IssuingAuthority)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(msa => msa.Description)
                .HasMaxLength(500);

            builder.HasIndex(msa => msa.DateOfAppreciation);
            builder.HasIndex(msa => msa.TitleOfAppreciation);
            builder.HasIndex(msa => msa.IssuingAuthority);

            #region FacultyMember Relationship
            builder.HasOne(msa => msa.FacultyMember)
                .WithMany(fm => fm.ManifestationsOfScientificAppreciations)
                .HasForeignKey(msa => msa.FacultyMemberId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region AttachmentsRelation

            builder.HasMany(m => m.Attachments)
                    .WithOne(a => a.ManifestationOfScientificAppreciation)
                    .HasForeignKey(a => a.ManifestationOfScientificAppreciationId)
                    .OnDelete(DeleteBehavior.Cascade);
            
            #endregion
        }
    }
}
