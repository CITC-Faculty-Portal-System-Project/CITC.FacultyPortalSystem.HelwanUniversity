namespace Domain.Entities.ResearchesModule.Theses_Supervision
{
    public class ThesesSupervision : BaseEntity<int>
    {
        public string Role { get; set; } = string.Empty;
        public DateOnly? RegistrationDate { get; set; }
        public DateOnly? SupervisionFormationDate { get; set; }
        public DateOnly? DiscussionDate { get; set; }
        public DateOnly? GrantingDate { get; set; }

        #region Relation With ThesesSupervisor
        public int ThesesSupervisorId { get; set; }
        #endregion

        #region Relation With Theses
        public int ThesesId { get; set; }
        #endregion

        #region Navigation Properties
        public ThesesSupervisor? ThesesSupervisor { get; set; }
        public Theses? Theses { get; set; }
        #endregion
    }
}
