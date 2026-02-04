using Domain.Entities.AcademicDataModule.ContributionsModule;

namespace Presistence.Data.Configurations.ContributionsModuleConfigurations
{
    public class ContributionsToCommunityServiceConfigurations : IEntityTypeConfiguration<ContributionsToCommunityService>
    {
        public void Configure(EntityTypeBuilder<ContributionsToCommunityService> builder)
        {
            builder.ToTable("ContributionsToCommunityServices");

            builder.HasKey(ctcs => ctcs.Id);

            builder.Property(ctcs => ctcs.ContributionTitle)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(ctcs => ctcs.Description)
                .HasMaxLength(500);

            builder.HasIndex(ctcs => ctcs.ContributionTitle);
            builder.HasIndex(ctcs => ctcs.DateOfContribution);

            #region FacultyMember Relationship
            builder.HasOne(ctcs => ctcs.FacultyMember)
                .WithMany(fm => fm.ContributionsToCommunityServices)
                .HasForeignKey(ctcs => ctcs.FacultyMemberId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

        }
    }
}
