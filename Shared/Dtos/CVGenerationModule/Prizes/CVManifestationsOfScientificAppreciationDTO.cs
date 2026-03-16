namespace Shared.Dtos.CVGenerationModule.Prizes
{
    public record CVManifestationsOfScientificAppreciationDTO
    {
        public int Id { get; set; }
        public string? TitleOfAppreciation { get; set; } 
        public string? IssuingAuthority { get; set; } 
        public DateOnly? DateOfAppreciation { get; set; }
    }
}
