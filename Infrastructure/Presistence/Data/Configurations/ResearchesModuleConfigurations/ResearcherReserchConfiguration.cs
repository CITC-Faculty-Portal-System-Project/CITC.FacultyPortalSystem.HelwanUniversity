using Domain.Entities.AcademicDataModule.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ResearcherReserchConfiguration : IEntityTypeConfiguration<ResearcherResearch>
    {
        public void Configure(EntityTypeBuilder<ResearcherResearch> builder)
        {

            builder.HasOne(rr => rr.Researcher)
                    .WithMany(rr => rr.ExternalResearches) 
                    .HasForeignKey(rr => rr.ResearcherId)
                    .OnDelete(DeleteBehavior.Cascade);

            
            builder.HasOne(rr => rr.ExternalResearch)
                  .WithMany(rr => rr.Researchers)
                  .HasForeignKey(rr => rr.ExternalResearchId)
                  .OnDelete(DeleteBehavior.Cascade);


            builder.HasIndex(rr => new { rr.ResearcherId, rr.ExternalResearchId });
        }
    }
}
