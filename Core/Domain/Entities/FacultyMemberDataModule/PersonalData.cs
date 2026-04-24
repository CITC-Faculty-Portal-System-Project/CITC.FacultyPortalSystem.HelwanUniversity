using Domain.Entities.EntitesAttachments;
using Domain.Entities.UniversityFacultiesAndDepartments;

namespace Domain.Entities.FacultyMemberDataModule
{
    public class PersonalData : BaseEntity<int>
    {
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;

        public Guid TitleId { get; set; }
        public Lookup Title { get; set; } = null!;

        public Guid GenderId { get; set; }
        public Lookup Gender { get; set; } = null!;

        public Guid MaritalStatusId { get; set; }
        public Lookup MaritalStatus { get; set; } = null!;

        public DateOnly? BirthDate { get; set; }
        public string? BirthPlace { get; set; }

        public Guid UniversityId { get; set; }
        public Lookup University { get; set; } = null!;

        public int DeptId { get; set; }
        public Department Department { get; set; } = null!;

        public int? FacultyId { get; set; }
        public Faculty Faculty { get; set; } = null!;


        public Guid AuthorityId { get; set; }
        public Lookup Authority { get; set; } = null!;

        public Guid FieldId { get; set; }
        public Lookup Field { get; set; } = null!;

        public string? GeneralSpecialization { get; set; }
        public string? AccurateSpecialization { get; set; }

        public string? NameInComposition { get; set; }
        public string? CompositionTopics { get; set; }

        public string? BioSummary { get; set; }
        public string? Skills { get; set; }

        #region Relation With FacultyMember
        public Guid FacultyMemberId { get; set; }
        #endregion

        #region Navigation Properties
        public FacultyMember? FacultyMember { get; set; }

        [NotMapped]
        public ProfilePictures? ProfilePicture { get; set; }
        #endregion
    }
}

