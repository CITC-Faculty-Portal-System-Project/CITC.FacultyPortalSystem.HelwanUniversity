using Domain.Contracts;

namespace Domain.Entities.AcademicDataModule.ResearchesModule
{
    public class ResearcherCoAuthor : IAuditablFields
    {
        public int ResearcherId { get; set; }
        public ResearcherProfile? Researcher { get; set; }

        public int CoAuthorId { get; set; }
        public CoAuthor? CoAuthor { get; set; }


        #region AuditFields

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
