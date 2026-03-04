using Domain.Entities.IdentityModule.Users;
namespace Domain.Entities.IdentityModule.Authorization
{
    public class RolePermission : PermissionAuditableFields
    {
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }

        public int PermissionId { get; set; }
        public Permission? Permission { get; set; }


    }
}