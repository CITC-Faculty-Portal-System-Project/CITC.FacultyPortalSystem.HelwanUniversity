using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.FacultyMemberData
{
    public class FacultyMember : BaseEntity<Guid>
    {
        public string NationalNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        #region Relation With Specialization
        public int? SpecializationId { get; set; }
        #endregion

        #region Navigations
        public PersonalData? PersonalData { get; set; } 
        public ContactData? ContactData { get; set; }
        public IdentificationCard? IdentificationCard { get; set; }
        public SocialMediaPlatforms? SocialMediaPlatforms { get; set; }
        public Specialization? Specialization { get; set; }
        #endregion
    }
}
