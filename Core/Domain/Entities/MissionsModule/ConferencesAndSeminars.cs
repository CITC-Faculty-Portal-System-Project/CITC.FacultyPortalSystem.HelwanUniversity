using Domain.Entities.FacultyMemberDataModule;

namespace Domain.Entities.MissionsModule
{
    public class ConferencesAndSeminars : BaseEntity<int>
    {
        public ConferenceOrSeminar Type { get; set; }
        public LocalOrInternational LocalOrInternational { get; set; }
        public string Name { get; set; } = string.Empty;

        public Guid RoleOfParticipationId { get; set; }
        public Lookup RoleOfParticipation { get; set; } = null!;

        public string OrganizingAuthority {  get; set; } = string.Empty;
        public string? Website { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string Venue {  get; set; } = string.Empty;
        public string? Notes {  get; set; } = string.Empty;

        #region Relation With FacultyMember
        public Guid FacultyMemberId { get; set; }
        #endregion

        #region Navigation Properties
        public FacultyMember? FacultyMember { get; set; }
        #endregion
    }
}
