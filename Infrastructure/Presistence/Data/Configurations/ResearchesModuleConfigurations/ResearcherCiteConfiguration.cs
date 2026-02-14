using Domain.Entities.AcademicDataModule.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ResearcherCiteConfiguration : IEntityTypeConfiguration<ResearcherCite>
    {
        public void Configure(EntityTypeBuilder<ResearcherCite> builder)
        {

            #region ConfiguringProperties
            
            builder.Property(e => e.Year)
                    .HasMaxLength(10)   
                    .IsRequired();

            builder.Property(e => e.NoOfCitations)
                   .HasDefaultValue(0)
                   .IsRequired();

            #endregion

            #region ConfiguringRelations

            builder.HasOne(rc => rc.Researcher)
                    .WithMany(r => r.ResearcherCites)
                    .HasForeignKey(rc => rc.ResearcherId)
                    .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region AddingIndecies

            builder.HasIndex(rc => rc.ResearcherId);

            #endregion
        }
    }
}
