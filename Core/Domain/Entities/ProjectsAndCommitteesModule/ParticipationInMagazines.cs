using Domain.Entities.FacultyMemberDataModule;

namespace Domain.Entities.ProjectsAndCommitteesModule
{
    public class ParticipationInMagazines : BaseEntity<int>
    {
        public string NameOfMagazine { get; set; } = string.Empty;
        public string? WebsiteOfMagazine { get; set; } 

        public Guid TypeOfParticipationId { get; set; }
        public Lookup TypeOfParticipation { get; set; } = null!;

        #region Relation With FacultyMember
        public Guid FacultyMemberId { get; set; }
        #endregion

        #region Navigation Properties
        public FacultyMember? FacultyMember { get; set; }
        #endregion
    }
}
