namespace Domain.Entities.ResearchesModule
{
    public class ResearchCite : BaseEntity<int>
    {
        public string Year { get; set; } = string.Empty;
        public int NoOfCitations { get; set; }
        public int ResearcherId { get; set; }
        public Researcher? Researcher { get; set; }
    }
}
