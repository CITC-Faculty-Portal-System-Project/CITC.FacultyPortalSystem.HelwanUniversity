using Domain.Entities.IdentityModule.Users;

namespace Domain.Entities.IdentityModule.Authorization
{
    public class UserPermission : PermissionAuditableFields
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public int PermissionId { get; set; }
        public Permission? Permission { get; set; }


    }
}
