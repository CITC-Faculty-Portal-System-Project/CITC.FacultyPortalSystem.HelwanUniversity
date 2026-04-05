using Domain.Entities.Messaging;

namespace Presistence.Data.Configurations.MessagingAndChattingModuleConfigurations
{
    public class ConversationParticipantConfigurations : IEntityTypeConfiguration<ConversationParticipant>
    {
        public void Configure(EntityTypeBuilder<ConversationParticipant> builder)
        {
            #region AddingKeys

            builder.HasKey(cp => new { cp.UserId, cp.ConversationId });

            #endregion

            #region ConfiguringRelations

            builder.HasOne(cp => cp.Conversation)
              .WithMany(c => c.Participants)
              .HasForeignKey(u => u.ConversationId)
              .OnDelete(DeleteBehavior.Cascade);


            #endregion

            #region AddingIndcies

            builder.HasIndex(cp => new { cp.UserId, cp.ConversationId })
                .HasDatabaseName("IX_ConversationParticipants_UserId_ConversationId");


            #endregion
        }
    }
}
