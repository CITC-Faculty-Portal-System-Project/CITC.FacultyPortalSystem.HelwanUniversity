namespace Domain.Entities.AcademicDataModule.ResearchesModule
{
    public class ResearcherCite : BaseEntity<int>
    {
        public string Year { get; set; } = string.Empty;
        public int NoOfCitations { get; set; }
        public int ResearcherId { get; set; }
        public ResearcherProfile? Researcher { get; set; }
    }
}
