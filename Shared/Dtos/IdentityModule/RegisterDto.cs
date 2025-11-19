using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.IdentityModule
{
    public record RegisterDto
    {
        [Required(ErrorMessage = "National Number Can't be Empty | لا يمكن ان يكون الرقم القومي فارغ")]
        public string NationalNumber { get; set; } = string.Empty;
    }
}
