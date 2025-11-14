namespace Domain.Entities.FacultyMemberData
{
    public class IdentificationCard : BaseEntity<int>
    {
        public string? ORCID { get; set; }
        public string? EKB { get; set; }
        public string? ResearcherId { get; set; }
        public string? ResearcherGate { get; set; }
        public string? AcademiaEdu { get; set; }

        #region Relation With FacultyMember
        public Guid FacultyMemberId { get; set; }
        #endregion

        #region Navigation Properties
        public FacultyMember? FacultyMember { get; set; }
        #endregion
    }
}
