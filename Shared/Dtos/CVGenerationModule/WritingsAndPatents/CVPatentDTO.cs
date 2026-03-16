namespace Shared.Dtos.CVGenerationModule.WritingsAndPatents
{
    public record CVPatentDTO
    {
        public int Id { get; set; }
        public string? NameOfPatent { get; set; } 
        public string? AccreditingAuthorityOrCountry { get; set; }
        public DateOnly? AccreditationDate { get; set; }
    }
}
