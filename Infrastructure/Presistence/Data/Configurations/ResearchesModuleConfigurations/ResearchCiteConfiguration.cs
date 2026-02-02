using Domain.Entities.AcademicDataModule.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ResearchCiteConfiguration : IEntityTypeConfiguration<ResearchCite>
    {
        public void Configure(EntityTypeBuilder<ResearchCite> builder)
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
                    .WithMany(r => r.ResearchCites)
                    .HasForeignKey(rc => rc.ResearcherId)
                    .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region AddingIndecies

            builder.HasIndex(rc => rc.ResearcherId);

            #endregion
        }
    }
}
