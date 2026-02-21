namespace Shared.Dtos.IdentityModule
{
    public record RegisterDto
    {
        public string NationalNumber { get; set; } = string.Empty;
    }
}
