namespace Shared.Dtos
{
    public record FacultyResponseDTO
    {
        public int Id { get; set; }
        public string NameAR { get; set; } = string.Empty;
        public string NameEN { get; set; } = string.Empty;
    }
}
