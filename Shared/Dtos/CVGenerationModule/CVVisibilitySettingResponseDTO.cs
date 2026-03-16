namespace Shared.Dtos.CVGenerationModule
{
    public record CVVisibilitySettingResponseDTO
    {
        public Guid Id { get; set; }
        public string VisibilityJson { get; set; } = "{}";
    }
}
