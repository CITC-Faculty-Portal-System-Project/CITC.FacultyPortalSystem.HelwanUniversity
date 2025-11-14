
namespace Presistence.Data.Configurations.FacultyMemberDataConfigurations
{
    public class IdentificationCardConfigurations : IEntityTypeConfiguration<IdentificationCard>
    {
        public void Configure(EntityTypeBuilder<IdentificationCard> builder)
        {
            builder.Property(ic => ic.ORCID)
                .HasColumnType("NVARCHAR(Max)")
                .IsRequired(false);
            builder.Property(ic => ic.EKB)
                .HasColumnType("NVARCHAR(Max)")
                .IsRequired(false);
            builder.Property(ic => ic.ResearcherId)
                .HasColumnType("NVARCHAR(Max)")
                .IsRequired(false);
            builder.Property(ic => ic.ResearcherGate)
                .HasColumnType("NVARCHAR(Max)")
                .IsRequired(false);
            builder.Property(ic => ic.AcademiaEdu)
                .HasColumnType("NVARCHAR(Max)")
                .IsRequired(false);
            builder.ToTable("IdentificationCards");

            #region Relation With FacultyMember
            builder.HasOne(ic => ic.FacultyMember)
               .WithOne(fm => fm.IdentificationCard)
               .HasForeignKey<IdentificationCard>(ic => ic.FacultyMemberId)
               .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
