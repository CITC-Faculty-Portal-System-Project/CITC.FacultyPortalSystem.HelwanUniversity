namespace Domain.Entities.ResearchesModule.Theses_Supervision
{
    public class ThesesSupervisor : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string JobLevel { get; set; } = string.Empty;
        public string Authority { get; set; } = string.Empty;
        #region Relation With FacultyMember
        public Guid? FacultyMemberId { get; set; }
        #endregion

        #region Navigation Properties
        public ICollection<ThesesSupervision> ThesesSupervisions { get; set; } = new HashSet<ThesesSupervision>();
        public FacultyMember? FacultyMember { get; set; }
        #endregion
    }
}
