using Domain.Entities.AcademicDataModule.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class CoAuthorConfigurations : IEntityTypeConfiguration<CoAuthor>
    {
        public void Configure(EntityTypeBuilder<CoAuthor> builder)
        {
            #region AddingIndecies

            builder.HasIndex(c => c.ScholarProfileLink);
            builder.HasIndex(c => c.AcademicName);
            builder.HasIndex(c => c.OrganisationalDomain);

            #endregion

            #region ConfiguringRelations

            builder.HasMany(c => c.Researchers)
                .WithOne(r => r.CoAuthor)
                .HasForeignKey(r => r.CoAuthorId)
                .OnDelete(DeleteBehavior.Cascade);
            
            #endregion
        }
    }
}
