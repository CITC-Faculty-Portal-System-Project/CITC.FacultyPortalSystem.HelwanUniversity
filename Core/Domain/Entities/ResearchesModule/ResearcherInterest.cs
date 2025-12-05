namespace Domain.Entities.ResearchesModule
{
    public class ResearcherInterest : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public int ResearcherId { get; set; }
        public Researcher? Researcher { get; set; }
        public int ExternalResearchId { get; set; }
        public ExternalResearch? ExternalResearch { get; set; }

    }
}
