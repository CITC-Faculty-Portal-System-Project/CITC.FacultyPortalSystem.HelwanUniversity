namespace Shared.Dtos.IdentityModule
{
    public record UserEditDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NationalNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
