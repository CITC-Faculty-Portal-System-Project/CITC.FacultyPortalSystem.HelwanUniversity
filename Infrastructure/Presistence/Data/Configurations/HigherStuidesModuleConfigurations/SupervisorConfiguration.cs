using Domain.Entities.HigherStuidesModule;

namespace Presistence.Data.Configurations.HigherStuidesModuleConfigurations
{
    public class SupervisorConfiguration : IEntityTypeConfiguration<Supervisor>
    {
        public void Configure(EntityTypeBuilder<Supervisor> builder)
        {

            #region ConfigruingProperties

            builder.Property(s => s.Role)
                   .HasConversion<string>();

            builder.Property(s => s.Name)
                 .HasMaxLength(250)
                 .IsRequired();

            builder.Property(s => s.Authority)
                   .HasMaxLength(500)
                   .IsRequired();

            #endregion

            #region ConfiguringRelations
            
            builder.HasOne(s => s.JobLevel)
                  .WithMany()
                  .HasForeignKey(s => s.JobLevelId)
                  .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region AddingIndecies
           
            builder.HasIndex(s => s.Name);

            #endregion

        }
    }
}
