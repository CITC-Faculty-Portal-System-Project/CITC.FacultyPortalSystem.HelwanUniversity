using Domain.Entities.FacultyMemberDataModule;

namespace Domain.Entities.MissionsModule
{
    public class TrainingPrograms : BaseEntity<int>
    {
        public TrainingProgramType Type { get; set; }
        public TrainingProgramParticipationType ParticipationType { get; set; } 
        public string TrainingProgramName { get; set; } = string.Empty;
        public string OrganizingAuthority { get; set; } = string.Empty;
        public string Venue { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Description { get; set; } = string.Empty;

        #region Relation With FacultyMember
        public Guid FacultyMemberId { get; set; }
        #endregion

        #region Navigation Properties
        public FacultyMember? FacultyMember { get; set; }
        #endregion
    }
}
