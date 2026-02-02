using Domain.Entities.AcademicDataModule.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    internal class InternalSystemResearchContributorsResearchesConfiguration : IEntityTypeConfiguration<InternalSystemResearchContributorsResearches>
    {
        public void Configure(EntityTypeBuilder<InternalSystemResearchContributorsResearches> builder)
        {
            builder.HasOne(ss => ss.InternalSystemResearchContributor)
                    .WithMany(s => s.Researches)
                    .HasForeignKey(ss => ss.InternalSystemResearchContributorId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ss => ss.InternalSystemResearch)
                    .WithMany(s => s.Contributors)
                    .HasForeignKey(ss => ss.InternalSystemResearchId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(ss => new { ss.InternalSystemResearchContributorId, ss.InternalSystemResearchId });
        }
    }
}
