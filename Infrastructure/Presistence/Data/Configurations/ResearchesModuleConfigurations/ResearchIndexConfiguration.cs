using Domain.Entities.AcademicDataModule.ResearchesModule;
using Microsoft.Extensions.DependencyInjection;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ResearchIndexConfiguration : IEntityTypeConfiguration<ResearchIndex>
    {
        public void Configure(EntityTypeBuilder<ResearchIndex> builder)
        {

            #region ConfiguringProperties

            builder.Property(e => e.PlatForm)
                    .HasMaxLength(100)
                    .IsRequired();

            #endregion

            #region AddingRelations

            builder.HasOne(ri => ri.ExternalResearch)
                    .WithMany(ri => ri.Indcies)
                    .HasForeignKey(ri => ri.ExternalResearchId)
                    .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region AddingIndcies

            builder.HasIndex(ri => ri.PlatForm);

            #endregion
        }
    }
}
