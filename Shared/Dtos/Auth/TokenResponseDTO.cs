namespace Shared.Dtos.Auth
{
    public record TokenResponseDTO
    {
        public string Token { get; set; } = string.Empty;
    }
}
