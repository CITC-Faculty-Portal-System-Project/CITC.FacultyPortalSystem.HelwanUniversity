namespace Domain.Entities.ResearchesModule
{
    public class ExternalResearch : BaseEntity<int>
    {
        public string DOI { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string PubYear { get; set; } = string.Empty;
        public string PubDate { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public int NoOfCititations { get; set; }
        public bool IsConfirmed { get; set; }
        public ICollection<ResearcherResearch>? Researchers { get; set; }
    }
}
