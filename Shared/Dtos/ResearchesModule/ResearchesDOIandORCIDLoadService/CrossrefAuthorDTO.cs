namespace Shared.Dtos.ResearchesModule.ResearchesDOIandORCIDLoadService
{
    public record CrossrefAuthorDTO
    {
        public string? Given { get; set; }
        public string? Family { get; set; }
        public string? Name { get; set; }
        public string? ORCID { get; set; }
    }
}
