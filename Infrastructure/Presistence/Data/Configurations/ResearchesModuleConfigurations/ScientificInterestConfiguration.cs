using Domain.Entities.AcademicDataModule.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ScientificInterestConfiguration : IEntityTypeConfiguration<ScientificInterest>
    {
        public void Configure(EntityTypeBuilder<ScientificInterest> builder)
        {
            #region Configuring Relations

            builder.HasMany(si => si.Researchers)
                .WithOne(r => r.Interest)
                .HasForeignKey(r => r.InterestId)
                .OnDelete(DeleteBehavior.Cascade);
            
            #endregion
        }
    }
}
