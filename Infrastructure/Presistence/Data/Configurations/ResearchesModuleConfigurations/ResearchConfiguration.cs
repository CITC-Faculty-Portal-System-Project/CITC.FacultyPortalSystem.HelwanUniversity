
using Domain.Entities.AcademicDataModule.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ResearchConfiguration : IEntityTypeConfiguration<Research>
    {
        public void Configure(EntityTypeBuilder<Research> builder)
        {

            #region ConfiguringEntityColumns

            builder.Property(e => e.DOI)
                    .HasMaxLength(150);

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

            builder.Property(e => e.PubDate)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(e => e.Publisher)
                   .HasMaxLength(500);

            builder.Property(e => e.NoOfCititations);

        
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

            builder.HasOne(r => r.Thesis)
                .WithMany(th => th.Researches)
                .HasForeignKey(r => r.ThesisId)
                .OnDelete(DeleteBehavior.SetNull);


            #endregion

            #region AddingIndcies

            builder.HasIndex(r => r.Source);
            builder.HasIndex(r => r.ResearchDerivedFrom);
            builder.HasIndex(r => r.PublisherType);
            builder.HasIndex(r => r.PublicationType);
            builder.HasIndex(r => r.PublicationType);
            builder.HasIndex(r => r.Title);
            builder.HasIndex(r => r.JournalOrConfernce);
            builder.HasIndex(r => r.PubYear);
            builder.HasIndex(r => r.DOI).IsUnique();
            
            #endregion
        }
    }
}
