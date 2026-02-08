namespace Shared.Dtos.ResearchesModule.ResearchesDOIandORCIDLoadService
{
    public record ResearcherDataGetByORCIDResponseDTO
    {
        public string? Orcid { get; set; }
        public string? OpenAlexId { get; set; }
        public string? Name { get; set; }
    }
}
