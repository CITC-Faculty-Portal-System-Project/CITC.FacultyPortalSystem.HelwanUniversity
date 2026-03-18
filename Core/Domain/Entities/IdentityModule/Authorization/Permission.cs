namespace Domain.Entities.IdentityModule.Authorization
{
    public class Permission : BaseEntity<int>
    {
        public string Code { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public PermissionType Type { get; set; }


        #region NavigationsAndRelations

        public ICollection<UserPermission>? Users { get; set; } = new List<UserPermission>();
        public ICollection<RolePermission>? Roles { get; set; } = new List<RolePermission>();

        #endregion
    }
}
