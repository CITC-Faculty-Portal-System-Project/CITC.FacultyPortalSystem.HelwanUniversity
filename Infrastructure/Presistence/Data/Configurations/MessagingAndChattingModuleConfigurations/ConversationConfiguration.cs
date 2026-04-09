using Domain.Entities.AdminModule;
using Domain.Entities.Messaging;

namespace Presistence.Data.Configurations.MessagingAndChattingModuleConfigurations
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            #region ConfiguringProperties

            builder.Property(c => c.Type)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(c => c.Title)
                   .IsRequired(false);


            #endregion

            #region ConfiguringRelations

            builder.HasMany(c => c.Participants)
                .WithOne(p => p.Conversation)
                .HasForeignKey(c => c.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(c => c.Messages)
              .WithOne(p => p.Conversation)
              .HasForeignKey(c => c.ConversationId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Ticket)
                    .WithOne(m => m.Conversation)
                    .HasForeignKey<Conversation>(m => m.TicketId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(th => th.Attachments)
                 .WithOne(a => a.Conversation)
                 .HasForeignKey(a => a.ConversationId)
                 .OnDelete(DeleteBehavior.Cascade);



            #endregion

            #region AddingIndecies


            builder.HasIndex(c => c.Type);
            builder.HasIndex(c => c.Id);
            builder.HasIndex(c => c.Title);
           
            #endregion
        }
    }
}
