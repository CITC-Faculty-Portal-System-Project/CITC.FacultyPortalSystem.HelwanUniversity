namespace Shared.Dtos.DataFetchingFromExternalService
{
    public record ExternalResearchCitesFetchingDTO
    {
        public int Year { get; set; }
        public int NumberOfCites { get; set; }

    }
}
