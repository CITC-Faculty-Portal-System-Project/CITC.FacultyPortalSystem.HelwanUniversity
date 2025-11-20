using Domain.Entities.FacultyMemberDataModule;

namespace Domain.Entities.ScientificProgressionModule
{
    public class AdministrativePositions : BaseEntity<int>
    {
        public string Position { get; set; } = string.Empty;
        public DateOnly StartDate {  get; set; }
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
