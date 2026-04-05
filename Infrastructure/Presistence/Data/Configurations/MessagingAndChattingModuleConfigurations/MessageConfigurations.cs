using Domain.Entities.Messaging;

namespace Presistence.Data.Configurations.MessagingAndChattingModuleConfigurations
{
    public class MessageConfigurations : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            #region ConfiguringRelations

            builder.HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(c => c.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region AddingIndcies

            builder.HasIndex(m => new { m.ConversationId, m.Id })
                .HasDatabaseName("IX_Messages_ConversationId_Id");

            builder.HasIndex(m => new { m.SenderId, m.CreatedAt })
                .HasDatabaseName("IX_Messages_SenderId_CreatedAt");

            #endregion
        }
    }
}
