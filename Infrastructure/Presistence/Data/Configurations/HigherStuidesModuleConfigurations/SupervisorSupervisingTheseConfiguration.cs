using Domain.Entities.HigherStuidesModule;

namespace Presistence.Data.Configurations.HigherStuidesModuleConfigurations
{
    public class SupervisorSupervisingTheseConfiguration : IEntityTypeConfiguration<SupervisorThesesSupervising>
    {
        public void Configure(EntityTypeBuilder<SupervisorThesesSupervising> builder)
        {
            
            builder.HasOne(ss => ss.Theses)
                   .WithMany(t => t.Supervisors)
                   .HasForeignKey(ss => ss.ThesesId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(ss => ss.Supervisor)
                   .WithMany(s => s.Theses) 
                   .HasForeignKey(ss => ss.SupervisorId)
                   .OnDelete(DeleteBehavior.Cascade);

            
            builder.HasOne(ss => ss.Theses)
                   .WithMany()
                   .HasForeignKey(ss => ss.ThesesId);
        }
    }
}
