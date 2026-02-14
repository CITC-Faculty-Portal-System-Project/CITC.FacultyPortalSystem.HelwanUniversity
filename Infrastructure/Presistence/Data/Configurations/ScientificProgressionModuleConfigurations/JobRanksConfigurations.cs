
using Domain.Entities.AcademicDataModule.ScientificProgressionModule;

namespace Presistence.Data.Configurations.ScientificProgressionModuleConfigurations
{
    public class JobRanksConfigurations : IEntityTypeConfiguration<JobRanks>
    {
        public void Configure(EntityTypeBuilder<JobRanks> builder)
        {
            builder.ToTable("JobRanks");

            builder.HasKey(jr => jr.Id);

            builder.Property(jr => jr.Notes)
                   .HasMaxLength(500);

            builder.HasIndex(jr => jr.DateOfJobRank);

            #region Dropdown Relationships
            builder.HasOne(jr => jr.JobRank)
                   .WithMany()
                   .HasForeignKey(jr => jr.JobRankId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Relationship with Faculty Member
            builder.HasOne(jr => jr.FacultyMember)
                   .WithMany(f => f.JobRanks)
                   .HasForeignKey(jr => jr.FacultyMemberId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
