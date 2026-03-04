using Domain.Contracts;

namespace Domain.Entities.IdentityModule.Authorization
{
    public class PermissionAuditableFields : IAuditablFields 
    {
        public string AssignedBy { get; set; } = string.Empty;
        public DateTime AssignedAt { get; set; }
        public Guid AssignerId { get; set; }

        public string? GrantedBy { get; set; } = string.Empty;
        public DateTime? GrantedAt { get; set; }
        public Guid? GranterId { get; set; }


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