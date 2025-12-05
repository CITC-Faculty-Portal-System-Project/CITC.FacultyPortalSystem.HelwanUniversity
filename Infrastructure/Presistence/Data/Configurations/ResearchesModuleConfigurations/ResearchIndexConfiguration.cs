using Domain.Entities.ResearchesModule;
using Microsoft.Extensions.DependencyInjection;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ResearchIndexConfiguration : IEntityTypeConfiguration<ResearchIndex>
    {
        public void Configure(EntityTypeBuilder<ResearchIndex> builder)
        {
            builder.HasOne<Researcher>()
                    .WithMany()
                    .HasForeignKey(ri => ri.ResearcherId);

            builder.HasOne<ExternalResearch>()
                    .WithMany()
                    .HasForeignKey(ri => ri.ExternalResearchId);
        }
    }
}
