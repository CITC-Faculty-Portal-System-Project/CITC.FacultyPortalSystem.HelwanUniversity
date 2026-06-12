using Domain.Entities.IdentityModule.Authorization;
using Domain.Enums;

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

            //#region DataSeeding

            //var userId = new Guid("A9923638-8866-4A89-A9FE-9CF329CFC8F7");
            //var seedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            //var userPermissions = new List<UserPermission>();
            //var permissionId = 1;

            //foreach (PermissionType module in Enum.GetValues(typeof(PermissionType)))
            //{
            //    for (int i = 0; i < 4; i++)
            //    {
            //        userPermissions.Add(new UserPermission
            //        {
            //            UserId = userId,
            //            PermissionId = permissionId++,
            //            AssignedBy = "System",
            //            AssignedAt = seedDate,
            //            AssignerId = Guid.Empty,
            //            CreatedBy = "System",
            //            CreatedAt = seedDate,
            //            IsDeleted = false,
            //            VersionNo = 1
            //        });
            //    }
            //    if (module == PermissionType.Tickets)
            //    {
            //        for (int i = 0; i < 8; i++)
            //        {
            //            userPermissions.Add(new UserPermission
            //            {
            //                UserId = userId,
            //                PermissionId = permissionId++,
            //                AssignedBy = "System",
            //                AssignedAt = seedDate,
            //                AssignerId = Guid.Empty,
            //                CreatedBy = "System",
            //                CreatedAt = seedDate,
            //                IsDeleted = false,
            //                VersionNo = 1
            //            });
            //        }
            //    }
            //}

            //builder.HasData(userPermissions);

            //#endregion


        }
    }
}
