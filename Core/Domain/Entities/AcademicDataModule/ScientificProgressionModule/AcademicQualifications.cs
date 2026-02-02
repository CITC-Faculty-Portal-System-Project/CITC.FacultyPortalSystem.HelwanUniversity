using Domain.Entities.Attachments;

namespace Domain.Entities.AcademicDataModule.ScientificProgressionModule
{
    public class AcademicQualifications : BaseEntity<int>
    {
        public Guid QualificationId { get; set; }
        public Lookup Qualification { get; set; } = null!;
        public string Specialization { get; set; } = string.Empty;

        public Guid? GradeId { get; set; }
        public Lookup? Grade { get; set; } 

        public Guid DispatchId { get; set; }
        public Lookup DispatchType { get; set; } = null!;

        public string? UniversityOrFaculty { get; set; } = string.Empty;
        public string CountryOrCity { get; set; } = string.Empty;

        public DateOnly DateOfObtainingTheQualification { get; set; }

        #region Relation With FacultyMember
        public Guid FacultyMemberId { get; set; }
        #endregion

        #region Relation With Attachment
        public Guid? AttachmentId { get; set; }
        #endregion

        #region Navigation Properties
        public FacultyMember? FacultyMember { get; set; }
        public AttachmentReference? Attachment { get; set; }

        #endregion
    }
}
