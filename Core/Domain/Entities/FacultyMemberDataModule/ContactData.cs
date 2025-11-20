using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.FacultyMemberDataModule
{
    public class ContactData : BaseEntity<int>
    {
        [Phone]
        public string MainPhoneNumber { get; set; } = string.Empty;
        public string? WorkPhoneNumber { get; set; }
        public string? HomePhoneNumber { get; set; }
        [EmailAddress]
        public string OfficialEmail { get; set; } = string.Empty;
        [EmailAddress]
        public string? PersonalEmail { get; set; }
        [EmailAddress]
        public string? AlternativeEmail { get; set; }
        public string? FaxNumber { get; set; }
        public string? Address { get; set; }

        #region Relation With FacultyMember
        public Guid FacultyMemberId { get; set; }
        #endregion

        #region Navigation Properties
        public FacultyMember? FacultyMember { get; set; }
        #endregion
    }
}
