namespace Domain.Entities.ResearchesModule
{
    public class ResearchContribution : BaseEntity<int>
    {
        public string MemberOrcid { get; set; } = string.Empty;
        public string MemberPositionInSearch { get; set; } = string.Empty;
        public string MemberAcademicName { get; set; } = string.Empty;
        public int ExternalResearchId { get; set; }
        public ExternalResearch? ExternalResearch { get; set; }
    }
}
