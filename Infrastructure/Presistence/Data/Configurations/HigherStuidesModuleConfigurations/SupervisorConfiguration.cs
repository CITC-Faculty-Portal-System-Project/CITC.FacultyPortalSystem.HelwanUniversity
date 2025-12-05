using Domain.Entities.HigherStuidesModule;

namespace Presistence.Data.Configurations.HigherStuidesModuleConfigurations
{
    public class SupervisorConfiguration : IEntityTypeConfiguration<Supervisor>
    {
        public void Configure(EntityTypeBuilder<Supervisor> builder)
        {
            builder.Property(s => s.Role)
                   .HasConversion<string>();

            builder.HasOne(s => s.JobLevel)
                  .WithMany()
                  .HasForeignKey(s => s.JobLevelId)
                  .OnDelete(DeleteBehavior.Restrict);

                

            builder.HasIndex(s => s.Name);

        }
    }
}
