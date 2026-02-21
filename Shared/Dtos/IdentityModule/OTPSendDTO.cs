namespace Shared.Dtos.IdentityModule
{
    public record OTPSendDTO
    {
        public string Otp { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

    }
}
