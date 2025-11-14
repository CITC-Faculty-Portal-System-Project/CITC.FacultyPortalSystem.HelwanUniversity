using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.IdentityModule
{
    public class User : IdentityUser<Guid>
    {
        public string NationalNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
