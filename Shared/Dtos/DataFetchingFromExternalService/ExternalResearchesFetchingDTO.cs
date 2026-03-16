namespace Shared.Dtos.DataFetchingFromExternalService
{
    public record ExternalResearchesFetchingDTO
    {
        public string DOI { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int? PubYear { get; set; }
        public string? PubDate { get; set; } = string.Empty;
        public string Journal { get; set; } = string.Empty;
        public string? Publisher { get; set; }
        public int? NoOfCititations { get; set; }
        public string? NoOfPages { get; set; }
        public string? Volume { get; set; }
        public string? Number { get; set; }
        public string ResearchLink { get; set; } = string.Empty;
        public string RelatedResearchLink { get; set; } = string.Empty;
        public string Abstract { get; set; } = string.Empty;
        public string IsConfirmed { get; set; } = string.Empty;
        public ICollection<ExternalResearchCitesFetchingDTO>? Cites { get; set; }
        public ICollection<ExternalResearchContributionFetchingDTO>? Contributions { get; set; }

    }
}
