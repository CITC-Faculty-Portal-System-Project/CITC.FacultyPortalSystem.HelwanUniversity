using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.IdentityModule
{
    public record LoginDto
    {
        [Required(ErrorMessage = "Username Can't be Empty | لا يمكن ان يكون اسم المستخدم فارغ")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password Can't be Empty | لا يمكن ان تكون كلمة المرور فارغة")]
        public string Password { get; set; } = string.Empty;
    }
}
