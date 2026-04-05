using Domain.Entities.IdentityModule.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.IdentityModule.Users
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<RolePermission>? Permissions { get; set; } = new List<RolePermission>();
        public ICollection<UserRole> Users { get; set; } = new List<UserRole>();


    }
}
