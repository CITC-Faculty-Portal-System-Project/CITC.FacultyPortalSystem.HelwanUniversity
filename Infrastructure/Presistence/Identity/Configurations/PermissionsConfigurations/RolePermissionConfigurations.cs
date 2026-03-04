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

        }
    }
}
