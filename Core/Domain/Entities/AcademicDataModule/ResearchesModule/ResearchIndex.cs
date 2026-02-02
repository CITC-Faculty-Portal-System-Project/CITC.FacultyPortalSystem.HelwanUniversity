namespace Domain.Entities.AcademicDataModule.ResearchesModule
{
    public class ResearchIndex : BaseEntity<int>
    {
        public string PlatForm { get; set; } = string.Empty;
        public int ExternalResearchId { get; set; }
        public ExternalResearch? ExternalResearch { get; set; }

    }
}
