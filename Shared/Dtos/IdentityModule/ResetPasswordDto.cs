using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.IdentityModule
{
    public record ResetPasswordDto
    {
        [Required(ErrorMessage = "Password Can't Be Empty | يمكن ان تكون كلمة المرور فارغة")]
        [Compare("NewPasswordConifrmed", ErrorMessage = "Passwords Are Not Identical | كلمات المرور غير متطابقة")]
        public string NewPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password Can't Be Empty | يمكن ان تكون كلمة المرور فارغة")]
        [Compare("NewPassword", ErrorMessage = "Passwords Are Not Identical | كلمات المرور غير متطابقة")]
        public string NewPasswordConifrmed { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
