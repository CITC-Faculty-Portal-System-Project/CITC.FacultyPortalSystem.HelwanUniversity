using Domain.Entities.AcademicDataModule.ExperiencesModule;

namespace Presistence.Data.Configurations.ExperiencesModuleConfigurations
{
    public class GeneralExperiencesConfigurations : IEntityTypeConfiguration<GeneralExperiences>
    {
        public void Configure(EntityTypeBuilder<GeneralExperiences> builder)
        {
            builder.ToTable("GeneralExperiences", t => t.HasCheckConstraint("CK_GeneralExp_Dates", "[EndDate] >= [StartDate]"));

            builder.HasKey(ge => ge.Id);

            builder.Property(ge => ge.ExperienceTitle)
                .HasMaxLength(250)
                .IsRequired();
            
            builder.Property(ge => ge.Authority)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(ge => ge.CountryOrCity)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(ge => ge.Description)
                .HasMaxLength(500);

            builder.HasIndex(ge => ge.ExperienceTitle);
            builder.HasIndex(ge => ge.Authority);
            builder.HasIndex(ge => ge.CountryOrCity);
            builder.HasIndex(ge => ge.StartDate);

            #region Relationship With FacultyMember
            builder.HasOne(ge => ge.FacultyMember)
                .WithMany(fm => fm.GeneralExperiences)
                .HasForeignKey(ge => ge.FacultyMemberId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
