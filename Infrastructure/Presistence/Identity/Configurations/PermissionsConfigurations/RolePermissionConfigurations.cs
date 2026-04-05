using Domain.Entities.IdentityModule.Authorization;

namespace Presistence.Identity.Configurations.PermissionsConfigurations
{
    public class RolePermissionConfigurations : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
           
            #region ConfiguringKeys

            builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });

            #endregion

            #region ConfiguringRelations

            builder.HasOne(rp => rp.Role)
                .WithMany(rp => rp.Permissions)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(rp => rp.Permission)
                .WithMany(rp => rp.Roles)
                .HasForeignKey(rp => rp.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region DataSeeding

            #region DataSeeding

            var roleId = new Guid("10000000-0000-0000-0000-000000000001");

            builder.HasData(
                new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = 46 // Tickets.Read
                },
                new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = 47 // Tickets.Update
                },
                new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = 50 // Tickets.Reply
                },
                new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = 54 // Tickets.ChangeStatus
                },
                new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = 56 // Tickets.ViewAssigned
                }
            );

            #endregion

            #endregion

        }
    }
}
