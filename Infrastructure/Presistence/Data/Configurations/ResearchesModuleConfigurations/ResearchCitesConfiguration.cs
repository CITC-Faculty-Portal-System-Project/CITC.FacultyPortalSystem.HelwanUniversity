using Domain.Entities.AcademicDataModule.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ResearchCitesConfiguration : IEntityTypeConfiguration<ResearchCite>
    {
        public void Configure(EntityTypeBuilder<ResearchCite> builder)
        {
            #region ConfiguringRelations

            builder.HasOne(c => c.Research)
                   .WithMany(r => r.Cites)
                   .HasForeignKey(c => c.ResearchId)
                   .OnDelete(DeleteBehavior.Cascade);
            
            #endregion
        }
    }
}
