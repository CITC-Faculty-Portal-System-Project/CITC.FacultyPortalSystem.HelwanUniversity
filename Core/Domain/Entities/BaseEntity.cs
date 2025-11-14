using Domain.Contracts;

namespace Domain.Entities
{
    public class BaseEntity<TKey> : IAuditablFields where TKey : notnull //Specify a generic type parameter TKey for the primary key
    {
        public TKey Id { get; set; }
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
    }
}
