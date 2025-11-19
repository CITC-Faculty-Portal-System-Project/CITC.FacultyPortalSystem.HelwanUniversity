namespace Shared.Dtos.IdentityModule
{
    public class UserRegistrationClientDto
    {
        public bool Exists { get; set; }
        public string NationalNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
