
using Domain.Entities.AcademicDataModule.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ResearchContributionConfiguration : IEntityTypeConfiguration<ResearchContribution>
    {
        public void Configure(EntityTypeBuilder<ResearchContribution> builder)
        {

            #region ConfiguringProperties

      
            builder.Property(e => e.MemberAcademicName)
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(e => e.ContributorType)
                    .HasConversion<string>();

            builder.Property(e => e.IsConfirmed)
                .HasDefaultValue(false);



            #endregion

            #region ConfiguringRelations

            builder.HasOne(rc => rc.Research)
                    .WithMany(r => r.Contributions)
                    .HasForeignKey(c => c.ResearchId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rc => rc.Contributor)
                 .WithMany(r => r.ResearchContributions)
                 .HasForeignKey(c => c.ContributorId)
                 .OnDelete(DeleteBehavior.Cascade);


            #endregion

            #region AddingIndcies

            builder.HasIndex(rc => new { rc.ContributorId, rc.ResearchId })
                .IsUnique();
            
            #endregion
        
        }
    }
}
