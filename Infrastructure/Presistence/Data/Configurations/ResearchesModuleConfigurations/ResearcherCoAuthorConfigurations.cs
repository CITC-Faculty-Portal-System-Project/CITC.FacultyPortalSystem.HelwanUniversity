using Domain.Entities.AcademicDataModule.ResearchesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ResearcherCoAuthorConfigurations : IEntityTypeConfiguration<ResearcherCoAuthor>
    {
        public void Configure(EntityTypeBuilder<ResearcherCoAuthor> builder)
        {
            #region AddingKey

            builder.HasKey(rco => new { rco.ResearcherId, rco.CoAuthorId });

            #endregion

            #region ConfiguringRelations

            builder.HasOne(r => r.Researcher)
                    .WithMany(rc => rc.CoAuthors)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.CoAuthor)
                    .WithMany(rc => rc.Researchers)
                    .OnDelete(DeleteBehavior.Cascade);


            #endregion
        }
    }
}
