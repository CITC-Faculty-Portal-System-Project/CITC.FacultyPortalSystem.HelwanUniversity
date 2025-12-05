using Domain.Entities.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class InternalSystemResearchConfiguration : IEntityTypeConfiguration<InternalSystemResearch>
    {
        public void Configure(EntityTypeBuilder<InternalSystemResearch> builder)
        {


            #region ConfiguringEntityProperties

            builder.Property(e => e.DOI)
                  .HasMaxLength(150);

            builder.Property(e => e.Title)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(e => e.LinkWithOtherResearch)
                   .HasMaxLength(500);

            builder.Property(e => e.Publisher)
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(e => e.ResearchLink)
                   .HasMaxLength(500);

            builder.Property(e => e.MagazineOrConference)
                   .HasMaxLength(300)
                   .IsRequired();

            builder.Property(e => e.Issue)
                   .HasMaxLength(50);

            builder.Property(e => e.Summary)
                   .HasMaxLength(2000);

            builder.Property(e => e.Year)
                   .IsRequired();

            builder.Property(e => e.PublisherType)
                    .HasConversion<string>()
                    .IsRequired();

            builder.Property(e => e.PublicationType)
                   .HasConversion<string>()
                    .IsRequired();

            builder.Property(e => e.ResearchDerivedFrom)
                   .HasConversion<string>()
                   .IsRequired();


            #endregion

            #region ConfiguringRelationShips

            builder.HasOne(isr => isr.FacultyMember)
                   .WithMany(f => f.InternalSystemResearches)
                   .HasForeignKey(r => r.FacultyMemberId)
                   .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region AddingIndex
            
            builder.HasIndex(r => r.Year);

            #endregion

        }
    }
}
