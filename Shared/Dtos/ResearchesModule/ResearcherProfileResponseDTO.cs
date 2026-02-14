using Shared.Dtos.DataFetchingFromExternalService;

namespace Shared.Dtos.ResearchesModule
{
    public record ResearcherProfileResponseDTO
    {
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
        public ICollection<ExternalResearcherCitesFetchingDTO>? ResearcherCites { get; set; }
        public ICollection<ExternalResearcherInterestsFetchingDTO>? ResearcherInterests { get; set; }
    }
}
