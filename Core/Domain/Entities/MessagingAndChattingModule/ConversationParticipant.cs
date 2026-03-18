using Domain.Contracts;
using Domain.Entities.IdentityModule.Users;

namespace Domain.Entities.Messaging
{
    public class ConversationParticipant : IAuditablFields
    {
        public int ConversationId { get; set; }
        public Guid UserId { get; set; }
        public DateTime JoinedAt { get; set; }
        public string Username { get; set; } = string.Empty;


        #region NavigationsAndRelations
        public Conversation? Conversation { get; set; }

        #endregion

        #region AuditableFields

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public byte[]? RowVersion { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletionReason { get; set; }
        public int VersionNo { get; set; }


        #endregion

    }
}
