using Domain.Entities.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class InternalSystemResearchContributorConfiguration : IEntityTypeConfiguration<InternalSystemResearchContributor>
    {
        public void Configure(EntityTypeBuilder<InternalSystemResearchContributor> builder)
        {

            #region ConfiguringEntityProperties

            builder.Property(e => e.Name)
                  .HasMaxLength(250)
                  .IsRequired();

            builder.Property(e => e.IsFromHelwanUniversity)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(e => e.IsTheMajorResearcher)
                   .HasDefaultValue(false)
                   .IsRequired();

            #endregion

            #region AddingIndecies

            builder.HasIndex(isr => isr.Name);
            
            #endregion

        }
    }
}
