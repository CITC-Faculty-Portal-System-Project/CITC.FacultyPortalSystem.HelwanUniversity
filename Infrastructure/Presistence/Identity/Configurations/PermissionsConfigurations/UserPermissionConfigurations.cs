using Domain.Entities.IdentityModule.Authorization;

namespace Presistence.Identity.Configurations.PermissionsConfigurations
{
    public class UserPermissionConfigurations : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
           
            #region AddingKeys

            builder.HasKey(up => new { up.UserId, up.PermissionId });

            #endregion

            #region ConfiguringRelations

            builder.HasOne(rp => rp.User)
              .WithMany(rp => rp.Permissions)
              .HasForeignKey(rp => rp.UserId)
              .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(rp => rp.Permission)
                .WithMany(rp => rp.Users)
                .HasForeignKey(rp => rp.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);


            #endregion


        }
    }
}
