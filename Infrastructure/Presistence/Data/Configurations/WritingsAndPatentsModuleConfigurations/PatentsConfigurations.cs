using Domain.Entities.AcademicDataModule.WritingsAndPatents;

namespace Presistence.Data.Configurations.WritingsAndPatentsModuleConfigurations
{
    public class PatentsConfigurations : IEntityTypeConfiguration<Patents>
    {
        public void Configure(EntityTypeBuilder<Patents> builder)
        {
            builder.ToTable("Patents", p => p.HasCheckConstraint("CK_Patents_Dates", "[AccreditationDate] >= [ApplyingDate]"));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.LocalOrInternational)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(p => p.NameOfPatent)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.AccreditingAuthorityOrCountry)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.HasIndex(p => p.LocalOrInternational);
            builder.HasIndex(p => p.NameOfPatent);
            builder.HasIndex(p => p.AccreditingAuthorityOrCountry);
            builder.HasIndex(p => p.ApplyingDate);
            builder.HasIndex(p => p.AccreditationDate);

            #region FacultyMember Relationship
            builder.HasOne(p => p.FacultyMember)
                .WithMany(fm => fm.Patents)
                .HasForeignKey(p => p.FacultyMemberId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
