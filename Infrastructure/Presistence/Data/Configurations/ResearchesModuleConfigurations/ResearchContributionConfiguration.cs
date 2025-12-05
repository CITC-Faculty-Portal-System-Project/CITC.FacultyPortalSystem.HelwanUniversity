
using Domain.Entities.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ResearchContributionConfiguration : IEntityTypeConfiguration<ResearchContribution>
    {
        public void Configure(EntityTypeBuilder<ResearchContribution> builder)
        {
            
            builder.HasOne<Researcher>()
                    .WithMany()
                    .HasForeignKey(rc => rc.ResearcherId)
                    .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne<ExternalResearch>()
                    .WithMany()
                    .OnDelete(DeleteBehavior.Cascade);
            
            
            builder.HasIndex(rc => rc.MemberAcademicName);
        }
    }
}
