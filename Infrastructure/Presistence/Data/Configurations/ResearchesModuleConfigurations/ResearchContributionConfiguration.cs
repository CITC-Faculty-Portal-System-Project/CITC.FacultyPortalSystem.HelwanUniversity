
using Domain.Entities.AcademicDataModule.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ResearchContributionConfiguration : IEntityTypeConfiguration<ResearchContribution>
    {
        public void Configure(EntityTypeBuilder<ResearchContribution> builder)
        {

            #region ConfiguringProperties

            builder.Property(e => e.MemberOrcid)
                    .HasMaxLength(20)
                    .IsRequired();

            builder.Property(e => e.MemberPositionInSearch)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(e => e.MemberAcademicName)
                   .HasMaxLength(250)
                   .IsRequired();

            #endregion

            #region ConfiguringRelations

            builder.HasOne(rc => rc.ExternalResearch)
                    .WithMany(r => r.Contributions)
                    .HasForeignKey(c => c.ExternalResearchId)
                    .OnDelete(DeleteBehavior.Cascade);
            
            #endregion

            #region AddingIndcies
            
            builder.HasIndex(rc => rc.MemberAcademicName);
            
            #endregion
        
        }
    }
}
