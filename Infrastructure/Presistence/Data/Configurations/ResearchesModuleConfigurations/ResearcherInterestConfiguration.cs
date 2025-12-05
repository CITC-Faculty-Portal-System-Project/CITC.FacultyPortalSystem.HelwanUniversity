using Domain.Entities.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ResearcherInterestConfiguration : IEntityTypeConfiguration<ResearcherInterest>
    {
        public void Configure(EntityTypeBuilder<ResearcherInterest> builder)
        {
            builder.HasOne<Researcher>()
                   .WithMany()
                   .HasForeignKey(ri => ri.ResearcherId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne<ExternalResearch>()
                   .WithMany()
                   .HasForeignKey(ri => ri.ExternalResearchId);



        }
    }
}
