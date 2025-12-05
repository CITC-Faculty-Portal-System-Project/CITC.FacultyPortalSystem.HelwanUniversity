
using Domain.Entities.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ExternalResearchConfiguration : IEntityTypeConfiguration<ExternalResearch>
    {
        public void Configure(EntityTypeBuilder<ExternalResearch> builder)
        {

            #region ConfiguringEntityColumns

            builder.Property(e => e.DOI)
                    .HasMaxLength(150)
                    .IsRequired();

            builder.Property(e => e.Link)
                   .IsRequired();

            builder.Property(e => e.Title)
                   .HasMaxLength(800)
                   .IsRequired();

            builder.Property(e => e.Source)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(e => e.PubYear)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(e => e.PubDate)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(e => e.Publisher)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(e => e.NoOfCititations)
                   .IsRequired();

            builder.Property(e => e.IsConfirmed)
                   .HasDefaultValue(false);

            #endregion
        }
    }
}
