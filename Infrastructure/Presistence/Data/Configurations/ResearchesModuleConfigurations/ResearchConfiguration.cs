
using Domain.Entities.AcademicDataModule.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ResearchConfiguration : IEntityTypeConfiguration<Research>
    {
        public void Configure(EntityTypeBuilder<Research> builder)
        {

            #region ConfiguringEntityColumns

            builder.Property(e => e.DOI)
                    .HasMaxLength(150)
                    .IsRequired();

            builder.Property(e => e.PublisherType)
                .HasConversion<string>();

            builder.Property(e => e.PublicationType)
                .HasConversion<string>();

            builder.Property(e => e.Source)
                   .HasConversion<string>();

            builder.Property(e => e.ResearchDerivedFrom)
                   .HasConversion<string>();


            builder.Property(e => e.Title)
                   .HasMaxLength(800)
                   .IsRequired();

            builder.Property(e => e.PubYear)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(e => e.PubDate)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(e => e.Publisher)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(e => e.NoOfCititations)
                   .IsRequired();

            builder.Property(e => e.IsConfirmed)
                   .HasDefaultValue(false);

            #endregion

            #region Configuring RelationShips

            builder.HasMany(er => er.Contributions)
                .WithOne(r => r.Research)
                .HasForeignKey(r => r.ContributorId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(er => er.Cites)
                .WithOne(c => c.Research)
                .HasForeignKey (c => c.ResearchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Attachments)
              .WithOne(a => a.Research)
              .HasForeignKey(a => a.ResearchId)
              .OnDelete(DeleteBehavior.Cascade);


            #endregion
        }
    }
}
