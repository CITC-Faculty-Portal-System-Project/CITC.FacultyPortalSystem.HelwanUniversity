namespace Domain.Entities.AcademicDataModule.ResearchesModule
{
    public class InternalSystemResearch : BaseEntity<int>
    {
        public string? DOI { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? LinkWithOtherResearch { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string? ResearchLink { get; set; } = string.Empty;
        public string MagazineOrConference { get; set; } = string.Empty;
        public PublisherType PublisherType { get; set; }
        public string? Issue { get; set; } = string.Empty;
        public int? Pages { get; set; }
        public int Year { get; set; }
        public PublicationType PublicationType { get; set; }
        public ResearchDerivedFrom ResearchDerivedFrom { get; set; }
        public string? Summary { get; set; } = string.Empty;
        public Guid FacultyMemberId { get; set; }
        public FacultyMember? FacultyMember { get; set; }
        public ICollection<InternalSystemResearchContributorsResearches>? Contributors { get; set; }
    }
}
