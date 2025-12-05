using Domain.Entities.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ResearcherInterestConfiguration : IEntityTypeConfiguration<ResearcherInterest>
    {
        public void Configure(EntityTypeBuilder<ResearcherInterest> builder)
        {

            #region ConfiguringProperties

            builder.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

            #endregion

            #region ConfiguringRelations

            builder.HasOne(r => r.Researcher)
                   .WithMany(r => r.ResearcherInterests)
                   .HasForeignKey(ri => ri.ResearcherId)
                   .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region AddingIndcies

            builder.HasIndex(ri => ri.Name);

            #endregion
        }
    }
}
