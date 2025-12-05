using Domain.Entities.ResearchesModule;

namespace Presistence.Data.Configurations.ResearchesModuleConfigurations
{
    public class InternalSystemResearchConfiguration : IEntityTypeConfiguration<InternalSystemResearch>
    {
        public void Configure(EntityTypeBuilder<InternalSystemResearch> builder)
        {
            builder.HasOne<FacultyMember>()
                   .WithMany()
                   .HasForeignKey(r => r.FacultyMemberId)
                   .OnDelete(DeleteBehavior.Cascade);
                    

          
        }
    }
}
