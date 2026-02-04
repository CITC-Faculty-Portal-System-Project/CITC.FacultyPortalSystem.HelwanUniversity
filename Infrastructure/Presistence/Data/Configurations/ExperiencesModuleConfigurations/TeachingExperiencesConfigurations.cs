using Domain.Entities.AcademicDataModule.ExperiencesModule;

namespace Presistence.Data.Configurations.ExperiencesModuleConfigurations
{
    public class TeachingExperiencesConfigurations : IEntityTypeConfiguration<TeachingExperiences>
    {
        public void Configure(EntityTypeBuilder<TeachingExperiences> builder)
        {
            builder.ToTable("TeachingExperiences", t => t.HasCheckConstraint("CK_TeachingExp_Dates", "[EndDate] >= [StartDate]"));

            builder.HasKey(te => te.Id);

            builder.Property(te => te.CourseName)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(te => te.AcademicLevel)
                .HasMaxLength(250);

            builder.Property(te => te.UniversityOrFaculty)
                .HasMaxLength(250);

            builder.Property(te => te.Description)
                .HasMaxLength(500);

            builder.HasIndex(te => te.CourseName);
            builder.HasIndex(te => te.AcademicLevel);
            builder.HasIndex(te => te.UniversityOrFaculty);
            builder.HasIndex(te => te.StartDate);

            #region Relationship With FacultyMember
            builder.HasOne(te => te.FacultyMember)
                .WithMany(fm => fm.TeachingExperiences)
                .HasForeignKey(te => te.FacultyMemberId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
