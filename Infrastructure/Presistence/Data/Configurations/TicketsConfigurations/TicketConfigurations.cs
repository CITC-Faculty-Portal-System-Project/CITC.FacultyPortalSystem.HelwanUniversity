using Domain.Entities.AdminModule;
using Domain.Entities.Messaging;

namespace Presistence.Data.Configurations.TicketsConfigurations
{
    public class TicketConfigurations : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            #region ConfiguringProperties

            builder.Property(t => t.Type)
                .HasConversion<int>();

            builder.Property(t => t.Priority)
              .HasConversion<int>();

            builder.Property(t => t.Status)
                .HasConversion<int>();

            #endregion

            #region AddingIndecies

            builder.HasIndex(t => t.Id); 
            builder.HasIndex(t => t.Title); 
            builder.HasIndex(t => t.Type); 
            builder.HasIndex(t => t.Priority); 
            builder.HasIndex(t => t.Status);

            #endregion

        }
    }
}
