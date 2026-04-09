using Domain.Entities.AcademicDataModule.HigherStuidesModule;

namespace Presistence.Data.Configurations.HigherStuidesModuleConfigurations
{
    public class SupervisingConfiguration : IEntityTypeConfiguration<Supervising>
    {
        public void Configure(EntityTypeBuilder<Supervising> builder)
        {

            #region RelationsConfigurations

            builder.HasOne(s => s.FacultyMember)
                   .WithMany(f => f.Supervisings)
                   .HasForeignKey(s => s.FacultyMemberId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Grade)
                  .WithMany()
                  .HasForeignKey(s => s.GradeId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Thesis)
                 .WithMany(th => th.Supervisings)
                 .HasForeignKey(s => s.ThesisId)
                 .OnDelete(DeleteBehavior.Restrict);


            #endregion

            #region PropertiesConfiguration

            builder.Property(s => s.FacultyMemberRole)
                   .HasConversion<string>();

            builder.Property(s => s.Type)
                   .HasConversion<string>();

            builder.Property(t => t.Title)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(t => t.StudentName)
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(t => t.Specialization)
                   .HasMaxLength(250);

            builder.Property(t => t.UniversityOrFaculty)
                   .HasMaxLength(250);

            builder.Property(t => t.isConfirmed)
                  .HasDefaultValue(true);

            #endregion

            #region AddingIndcies

            builder.HasIndex(s => s.Title);
            builder.HasIndex(s => s.StudentName);
            builder.HasIndex(s => s.RegistrationDate);
            builder.HasIndex(s => s.RegistrationDate);
            builder.HasIndex(s => s.SupervisionFormationDate);
            builder.HasIndex(s => s.DiscussionDate);
            builder.HasIndex(s => s.GrantingDate);
            builder.HasIndex(s => s.GradeId);
            builder.HasIndex(s => s.Type);

            #endregion
        }
    }
}
