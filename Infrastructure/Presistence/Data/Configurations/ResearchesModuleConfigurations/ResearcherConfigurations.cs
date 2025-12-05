using Domain.Entities.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class ResearcherConfigurations : IEntityTypeConfiguration<Researcher>
    {
        public void Configure(EntityTypeBuilder<Researcher> builder)
        {
            builder.HasOne(r => r.FacultyMember)
                   .WithOne(r => r.Researcher)
                   .HasForeignKey<Researcher>(r=> r.FacultyMemberId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
