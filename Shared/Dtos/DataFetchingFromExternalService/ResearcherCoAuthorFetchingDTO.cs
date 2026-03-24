namespace Shared.Dtos.DataFetchingFromExternalService
{
    public record ResearcherCoAuthorFetchingDTO
    {
        public string ScholarProfileLink { get; set; } = string.Empty;
        public string AcademicName { get; set; } = string.Empty;
        public string? ScholarProfileImageURL { get; set; } = string.Empty;
        public string? OrganisationalDomain { get; set; } = string.Empty;
        public string? JobTitle { get; set; } = string.Empty;

    }
}
