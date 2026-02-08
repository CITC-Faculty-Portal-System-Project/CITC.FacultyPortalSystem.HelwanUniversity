
using Domain.Entities.AcademicDataModule.MissionsModule;

namespace Presistence.Data.Configurations.MissionsModuleConfigurations
{
    public class ConferencesAndSeminarsConfigurations : IEntityTypeConfiguration<ConferencesAndSeminars>
    {
        public void Configure(EntityTypeBuilder<ConferencesAndSeminars> builder)
        {
            builder.ToTable("ConferencesAndSeminars");

            builder.HasKey(cas => cas.Id);

            builder.Property(cas => cas.Type)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(cas => cas.LocalOrInternational)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(cas => cas.Name)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(cas => cas.OrganizingAuthority)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(cas => cas.Website)
                .HasColumnType("NVARCHAR(MAX)");

            builder.Property(cas => cas.Venue)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(cas => cas.Notes)
                .HasMaxLength(500);

            builder.HasIndex(cas => cas.Name);
            builder.HasIndex(cas => cas.StartDate);

            #region Dropdown Relationships
            builder.HasOne(cas => cas.RoleOfParticipation)
                   .WithMany()
                   .HasForeignKey(cas => cas.RoleOfParticipationId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Relationship with Faculty Member
            builder.HasOne(cas => cas.FacultyMember)
                   .WithMany(f => f.ConferencesAndSeminars)
                   .HasForeignKey(cas => cas.FacultyMemberId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion

        }
    }
}
