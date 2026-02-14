using Domain.Entities.FacultyMemberDataModule;

namespace Domain.Entities.AcademicDataModule.MissionsModule
{
    public class ScientificMissions : BaseEntity<int>
    {
        public string MissionName { get; set; } = string.Empty;
        public string? UniversityOrFaculty { get; set; } = string.Empty;
        public string CountryOrCity { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Notes { get; set; }

        #region Relation With FacultyMember
        public Guid FacultyMemberId { get; set; }
        #endregion

        #region Navigation Properties
        public FacultyMember? FacultyMember { get; set; }
        #endregion
    }
}
