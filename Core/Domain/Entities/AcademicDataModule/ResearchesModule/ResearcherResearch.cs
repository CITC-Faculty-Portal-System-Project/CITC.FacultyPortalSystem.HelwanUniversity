namespace Domain.Entities.AcademicDataModule.ResearchesModule
{
    public class ResearcherResearch : BaseEntity<int>
    {
        public int ResearcherId { get; set; }
        public Researcher? Researcher { get; set; }
        public int ExternalResearchId { get; set; }
        public ExternalResearch? ExternalResearch { get; set; }

    }
}
