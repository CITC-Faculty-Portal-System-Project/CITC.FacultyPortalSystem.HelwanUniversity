namespace Shared.Dtos.IdentityModule
{
    public record ResetPasswordDto
    {
        public string NewPassword { get; set; } = string.Empty;
        public string NewPasswordConifrmed { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
