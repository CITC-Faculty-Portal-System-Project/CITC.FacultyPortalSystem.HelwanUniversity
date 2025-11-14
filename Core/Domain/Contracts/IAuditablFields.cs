namespace Domain.Contracts
{
    public interface IAuditablFields
    {
        public string CreatedBy { get; set; } 
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
