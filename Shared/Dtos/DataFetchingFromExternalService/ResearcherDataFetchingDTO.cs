namespace Shared.Dtos.DataFetchingFromExternalService
{
    public record ResearcherDataFetchingDTO
    {
        public string NationalNumber { get; set; } = string.Empty;
        public string ORCID { get; set; } = string.Empty;
        public string ScholarProfileLink { get; set; } = string.Empty;
        public string ScholarProfileImageURL { get; set; } = string.Empty;
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
        public ICollection<ExternalResearchesFetchingDTO>? Researches { get; set; }
        public ICollection<ExternalResearcherInterestsFetchingDTO>? Interests { get; set; }
        public ICollection<ExternalResearcherCitesFetchingDTO>? ResearcherCites { get; set; }

    }
}
