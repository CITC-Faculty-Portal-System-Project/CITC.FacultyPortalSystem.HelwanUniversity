using Domain.Entities.EntitesAttachments;
using System.Reflection.PortableExecutable;

namespace Domain.Entities.AcademicDataModule.PrizesModule
{
    public class ManifestationsOfScientificAppreciation : BaseEntity<int>
    {
        public string TitleOfAppreciation { get; set; } = string.Empty;
        public string IssuingAuthority { get; set; } = string.Empty;
        public DateOnly DateOfAppreciation { get; set; }
        public string? Description { get; set; }

        #region Relation With FacultyMember
        public Guid FacultyMemberId { get; set; }
        #endregion

        #region Navigation Properties
        public FacultyMember? FacultyMember { get; set; }
        public ICollection<ManifestationsOfScientificAppreciationAttachment>? Attachments { get; set; } = new List<ManifestationsOfScientificAppreciationAttachment>();
        #endregion
    }
}