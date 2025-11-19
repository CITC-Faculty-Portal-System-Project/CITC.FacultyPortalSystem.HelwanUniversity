using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.IdentityModule
{
    public record OTPSendDTO
    {
        [Required(ErrorMessage = "Password Can't Be Empty | يمكن ان تكون كلمة المرور المؤقتة فارغة")]
        public string Otp { get; set; } = string.Empty;

    }
}
