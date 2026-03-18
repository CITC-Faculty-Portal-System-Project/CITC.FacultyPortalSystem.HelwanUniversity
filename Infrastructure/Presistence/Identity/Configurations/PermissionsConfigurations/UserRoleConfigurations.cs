using Domain.Entities.IdentityModule.Users;

namespace Presistence.Identity.Configurations.PermissionsConfigurations
{
    public class UserRoleConfigurations : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
           
            
            #region ConfigruingKey

            builder.HasKey(x => new { x.UserId, x.RoleId });

            #endregion


            #region ConfiguringRelations

            builder.HasOne(x => x.User)
                   .WithMany(u => u.Roles)
                   .HasForeignKey(x => x.UserId);

             builder.HasOne(x => x.Role)
                      .WithMany(r => r.Users)
                      .HasForeignKey(x => x.RoleId);

            #endregion
        
        }
    }
}
