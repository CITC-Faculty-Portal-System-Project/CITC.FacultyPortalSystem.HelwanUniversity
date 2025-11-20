using Domain.Entities.FacultyMemberDataModule;

namespace Domain.Entities.ScientificProgressionModule
{
    public class JobRanks : BaseEntity<int>
    {
        public Guid JobRankId { get; set; }
        public Lookup JobRank { get; set; } = null!;

        public DateOnly DateOfJobRank { get; set; }
        public string Notes { get; set; } = string.Empty;

        #region Relation With FacultyMember
        public Guid FacultyMemberId { get; set; }
        #endregion

        #region Navigation Properties
        public FacultyMember? FacultyMember { get; set; }
        #endregion
    }
}
