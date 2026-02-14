using Domain.Entities.AcademicDataModule.ContributionsModule;

namespace Presistence.Data.Configurations.ContributionsModuleConfigurations
{
    public class ContributionsToUniversityConfigurations : IEntityTypeConfiguration<ContributionsToUniversity>
    {
        public void Configure(EntityTypeBuilder<ContributionsToUniversity> builder)
        {
            builder.ToTable("ContributionsToUniversity");

            builder.HasKey(ctu => ctu.Id);

            builder.Property(ctu => ctu.ContributionTitle)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(ctu => ctu.Description)
                .HasMaxLength(1000);

            builder.HasIndex(ctu => ctu.DateOfContribution);
            builder.HasIndex(ctu => ctu.TypeOfContributionId);
            builder.HasIndex(ctu => ctu.ContributionTitle);

            #region Dropdown Relationship
            builder.HasOne(ctu => ctu.TypeOfContribution)
                .WithMany()
                .HasForeignKey(ctu => ctu.TypeOfContributionId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region FacultyMember Relationship
            builder.HasOne(ctu => ctu.FacultyMember)
                .WithMany(fm => fm.ContributionsToUniversity)
                .HasForeignKey(ctu => ctu.FacultyMemberId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
