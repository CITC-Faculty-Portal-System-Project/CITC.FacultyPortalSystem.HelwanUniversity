using Domain.Entities.AdminModule;

namespace Presistence.Data.Configurations.TicketsConfigurations
{
    public class TicketMessageConfigurations : IEntityTypeConfiguration<TicketMessage>
    {
        public void Configure(EntityTypeBuilder<TicketMessage> builder)
        {
            #region AddingIndecies

            builder.HasIndex(m => m.Id);
            builder.HasIndex(m => m.CreatedAt);

            #endregion

            #region ConfiguringRelations

            builder.HasOne(m => m.Ticket)
                .WithMany(t => t.Messages)
                .HasForeignKey(m => m.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
           
            #endregion
        }
    }
}
