
using Domain.Entities.AcademicDataModule.MissionsModule;

namespace Presistence.Data.Configurations.MissionsModuleConfigurations
{
    public class TrainingProgramsConfigurations : IEntityTypeConfiguration<TrainingPrograms>
    {
        public void Configure(EntityTypeBuilder<TrainingPrograms> builder)
        {
            builder.ToTable("TrainingPrograms", t => t.HasCheckConstraint("CK_TrainingPrograms_Dates", "[EndDate] >= [StartDate]"));

            builder.HasKey(tp => tp.Id);

            builder.Property(tp => tp.Type)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(tp => tp.ParticipationType)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(tp => tp.TrainingProgramName)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(tp => tp.OrganizingAuthority)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(tp => tp.Venue)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(tp => tp.Description)
                .HasMaxLength(500);

            builder.HasIndex(tp => tp.TrainingProgramName);
            builder.HasIndex(tp => tp.StartDate);

            #region Relationship with Faculty Member
            builder.HasOne(tp => tp.FacultyMember)
                   .WithMany(f => f.TrainingPrograms)
                   .HasForeignKey(tp => tp.FacultyMemberId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
