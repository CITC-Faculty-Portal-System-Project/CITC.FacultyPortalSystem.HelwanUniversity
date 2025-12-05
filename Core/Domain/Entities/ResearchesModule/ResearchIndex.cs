namespace Domain.Entities.ResearchesModule
{
    public class ResearchIndex : BaseEntity<int>
    {
        public string PlatForm { get; set; } = string.Empty;
        public int ResearcherId { get; set; }
        public Researcher? Researcher { get; set; }
        public int ExternalResearchId { get; set; }
        public ExternalResearch? ExternalResearch { get; set; }

    }
}
