namespace Shared.Dtos.DataFetchingFromExternalService
{
    public record ExternalResearcherCitesFetchingDTO
    {
        public int Year { get; set; }
        public int NoOfCitations { get; set; }

    }
}
