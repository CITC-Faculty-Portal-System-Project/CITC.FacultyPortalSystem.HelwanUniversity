
using Domain.Entities.AcademicDataModule.ScientificProgressionModule;

namespace Presistence.Data.Configurations.ScientificProgressionModuleConfigurations
{
    public class AdministrativePositionsConfigurations : IEntityTypeConfiguration<AdministrativePositions>
    {
        public void Configure(EntityTypeBuilder<AdministrativePositions> builder)
        {
            builder.ToTable("AdministrativePositions", t => t.HasCheckConstraint("CK_AdminPositions_Dates", "[EndDate] >= [StartDate]"));

            builder.HasKey(ap => ap.Id);

            builder.Property(ap => ap.Position)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(ap => ap.Notes)
                   .HasMaxLength(500);

            builder.HasIndex(ap => ap.Position);
            builder.HasIndex(ap => ap.StartDate);

            #region Relationship with Faculty Member
            builder.HasOne(ap => ap.FacultyMember)
                   .WithMany(f => f.AdministrativePositions)
                   .HasForeignKey(ap => ap.FacultyMemberId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
