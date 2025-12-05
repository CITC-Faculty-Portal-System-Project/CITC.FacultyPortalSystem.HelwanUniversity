using Domain.Entities.HigherStuidesModule;

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

            #endregion

            #region AddingIndcies

            builder.HasIndex(s => s.Title);
            builder.HasIndex(s => s.StudentName);

            #endregion


        }
    }
}
