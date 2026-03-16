using Domain.Entities.AcademicDataModule.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ResearcherInterestConfiguration : IEntityTypeConfiguration<ResearcherInterest>
    {
        public void Configure(EntityTypeBuilder<ResearcherInterest> builder)
        {

            #region ConfiguringRelations

            builder.HasOne(r => r.Researcher)
                   .WithMany(r => r.ResearcherInterests)
                   .HasForeignKey(ri => ri.ResearcherId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Interest)
              .WithMany(r => r.Researchers)
              .HasForeignKey(ri => ri.InterestId)
              .OnDelete(DeleteBehavior.Cascade);


            #endregion

            #region ConfiguringKey

                builder.HasKey(ri => new {ri.ResearcherId , ri.InterestId});

            #endregion
        }
    }
}
