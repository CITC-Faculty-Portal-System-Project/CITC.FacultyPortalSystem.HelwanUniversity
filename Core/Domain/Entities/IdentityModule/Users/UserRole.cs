using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.IdentityModule.Users
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
