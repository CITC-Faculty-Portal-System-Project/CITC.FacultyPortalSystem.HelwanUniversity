using Domain.Entities.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ResearchCiteConfiguration : IEntityTypeConfiguration<ResearchCite>
    {
        public void Configure(EntityTypeBuilder<ResearchCite> builder)
        {
            builder.HasOne<Researcher>()
                    .WithMany()
                    .HasForeignKey(rc => rc.ResearcherId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
