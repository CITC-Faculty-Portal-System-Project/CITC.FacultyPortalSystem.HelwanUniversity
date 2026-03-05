using Domain.Entities.AcademicDataModule.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ResearcherConfigurations : IEntityTypeConfiguration<ResearcherProfile>
    {
        public void Configure(EntityTypeBuilder<ResearcherProfile> builder)
        {


            #region PropertiesConfiguration

            builder.Property(e => e.ORCID)
                    .HasMaxLength(20)    
                    .IsRequired();

            builder.Property(e => e.ScholarProfileLink)
                   .HasMaxLength(500)  
                   .IsRequired();

            builder.Property(e => e.AcademicName)
                   .HasMaxLength(250) 
                   .IsRequired();

            builder.Property(e => e.OrganisationalDomain)
                   .HasMaxLength(150) 
                   .IsRequired();

            builder.Property(e => e.JobTitle)
                   .HasMaxLength(200) 
                   .IsRequired();

            builder.Property(e => e.OrganisationId)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(e => e.TotalNumberOfCitiations)
                   .IsRequired();

            builder.Property(e => e.NumberOfCitiationsInLastFiveYears)
                   .IsRequired();

            builder.Property(e => e.Hindex)
                   .IsRequired();

            builder.Property(e => e.HindexInLastFiveYears)
                   .IsRequired();

            builder.Property(e => e.I10index)
                   .IsRequired();

            builder.Property(e => e.I10index5y)
                   .IsRequired();

            #endregion

            #region ConfiguringRelations

            builder.HasOne(r => r.FacultyMember)
                   .WithOne(r => r.Researcher)
                   .HasForeignKey<ResearcherProfile>(r=> r.FacultyMemberId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.ResearcherInterests)
                    .WithOne(r => r.Researcher)
                    .HasForeignKey(r => r.ResearcherId)
                    .OnDelete(DeleteBehavior.Cascade);
         
            builder.HasMany(r => r.ResearcherCites)
                .WithOne(rc => rc.Researcher)
                .HasForeignKey(rc => rc.ResearcherId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(r => r.CoAuthors)
            .WithOne(rc => rc.Researcher)
            .HasForeignKey(rc => rc.ResearcherId)
            .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region AddingIndcies

            builder.HasIndex(r => r.AcademicName);

            #endregion
        }
    }
}
