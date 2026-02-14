namespace Shared.Dtos.ResearchesModule
{
    public record ResearchCitesResponseDTO
    {
        public int Year { get; set; }
        public int NumberOfCites { get; set; }
        public int Id { get; set; }
        public int ResearchId { get; set; }

    }
}
