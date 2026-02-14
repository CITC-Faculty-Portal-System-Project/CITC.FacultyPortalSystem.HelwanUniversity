
using Domain.Entities.AcademicDataModule.MissionsModule;

namespace Presistence.Data.Configurations.MissionsModuleConfigurations
{
    public class ScientificMissionsConfigurations : IEntityTypeConfiguration<ScientificMissions>
    {
        public void Configure(EntityTypeBuilder<ScientificMissions> builder)
        {
            builder.ToTable("ScientificMissions", t => t.HasCheckConstraint("CK_SciMissions_Dates", "[EndDate] >= [StartDate]"));

            builder.HasKey(sm => sm.Id);

            builder.Property(sm => sm.MissionName)
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(sm => sm.UniversityOrFaculty)
                   .HasMaxLength(250);

            builder.Property(sm => sm.CountryOrCity)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(sm => sm.Notes)
                .HasMaxLength(500);

            builder.HasIndex(sm => sm.MissionName);
            builder.HasIndex(sm => sm.CountryOrCity);
            builder.HasIndex(sm => sm.StartDate);

            #region Relationship with Faculty Member
            builder.HasOne(sm => sm.FacultyMember)
                   .WithMany(f => f.ScientificMissions)
                   .HasForeignKey(sm => sm.FacultyMemberId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
