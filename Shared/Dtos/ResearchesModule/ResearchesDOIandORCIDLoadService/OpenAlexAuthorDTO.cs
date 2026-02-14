namespace Shared.Dtos.ResearchesModule.ResearchesDOIandORCIDLoadService
{
    public record OpenAlexAuthorDTO
    {
        public string? Id { get; set; }
        public string? Display_Name { get; set; }
        public string? Orcid { get; set; }
    }
}
