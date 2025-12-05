namespace Domain.Entities.ResearchesModule
{
    public class Researcher : BaseEntity<int>
    {
        public string ORCID { get; set; } = string.Empty;
        public string ScholarProfileLink { get; set; } = string.Empty;
        public string AcademicName { get; set; } = string.Empty;
        public string OrganisationalDomain { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string OrganisationId { get; set; } = string.Empty;
        public int TotalNumberOfCitiations { get; set; }
        public int NumberOfCitiationsInLastFiveYears { get; set; }
        public int Hindex { get; set; }
        public int HindexInLastFiveYears { get; set; }
        public int I10index { get; set; }
        public int I10index5y { get; set; }
        public Guid FacultyMemberId { get; set; }
        public FacultyMember? FacultyMember { get; set; }
        public ICollection<ResearcherResearch>? ExternalResearches { get; set; }
        public ICollection<ResearchCite>? ResearchCites { get; set; }
        public ICollection<ResearcherInterest>? ResearcherInterests { get; set; }

    }
}
