using Domain.Entities.IdentityModule.Authorization;

namespace Presistence.Identity.Configurations.PermissionsConfigurations
{
    public class PermissionConfigurations : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
           
            
            #region ConfiguringProperties

            builder.Property(p => p.Type)
                   .HasConversion<int>();


            builder.Property(p => p.Code)
                   .IsRequired();

            builder.Property(p => p.DisplayName)
                  .IsRequired();

            builder.Property(p => p.Description)
                  .IsRequired(false);



            #endregion

            #region AddingIndices

            builder.HasIndex(p => p.Id);
            builder.HasIndex(p => p.DisplayName);
            builder.HasIndex(p => p.Code);
            builder.HasIndex(p => p.Type);

            #endregion

            #region ConfiguringRelations

            builder.HasMany(p => p.Roles)
                    .WithOne(u => u.Permission)
                    .HasForeignKey(u => u.PermissionId)
                    .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(p => p.Users)
                  .WithOne(u => u.Permission)
                  .HasForeignKey(u => u.PermissionId)
                  .OnDelete(DeleteBehavior.Cascade);


            #endregion

            #region DataSeeding

            var permissions = new List<Permission>();
            var id = 1;

            foreach (PermissionType module in Enum.GetValues(typeof(PermissionType)))
            {
                permissions.AddRange(new[]
                {
                    new Permission
                    {
                        Id = id++,
                        DisplayName = $"{module} - Create",
                        Code = $"{module}.Create",
                        Description = $"Enables Assignee to Create entities which {module} includes",
                        Type = module
                    },
                    new Permission
                    {
                        Id = id++,
                        DisplayName = $"{module} - Read",
                        Code = $"{module}.Read",
                        Description = $"Enables Assignee to Read entities which {module} includes",
                        Type = module
                    },
                    new Permission
                    {
                        Id = id++,
                        DisplayName = $"{module} - Update",
                        Code = $"{module}.Update",
                        Description = $"Enables Assignee to Update entities which {module} includes",
                        Type = module
                    },
                    new Permission
                    {
                        Id = id++,
                        DisplayName = $"{module} - Delete",
                        Code = $"{module}.Delete",
                        Description = $"Enables Assignee to Delete entities which {module} includes",
                        Type = module
                    }
                });

                // Extra permissions for Tickets module
                if (module == PermissionType.Tickets)
                {
                    permissions.AddRange(new[]
                    {
                    new Permission
                    {
                        Id = id++,
                        DisplayName = $"{module} - Assign",
                        Code = $"{module}.Assign",
                        Description = "Allows assigning tickets to support agents",
                        Type = module
                    },
                    new Permission
                    {
                        Id = id++,
                        DisplayName = $"{module} - Reply",
                        Code = $"{module}.Reply",
                        Description = "Allows replying to tickets",
                        Type = module
                    },
                    new Permission
                    {
                        Id = id++,
                        DisplayName = $"{module} - Close",
                        Code = $"{module}.Close",
                        Description = "Allows closing tickets",
                        Type = module
                    },
                    new Permission
                    {
                        Id = id++,
                        DisplayName = $"{module} - Reopen",
                        Code = $"{module}.Reopen",
                        Description = "Allows reopening tickets",
                        Type = module
                    },
                    new Permission
                    {
                        Id = id++,
                        DisplayName = $"{module} - Change Priority",
                        Code = $"{module}.ChangePriority",
                        Description = "Allows changing ticket priority",
                        Type = module
                    },
                    new Permission
                    {
                        Id = id++,
                        DisplayName = $"{module} - Change Status",
                        Code = $"{module}.ChangeStatus",
                        Description = "Allows changing ticket status",
                        Type = module
                    },
                    new Permission
                    {
                        Id = id++,
                        DisplayName = $"{module} - View All",
                        Code = $"{module}.ViewAll",
                        Description = "Allows viewing all tickets in the system",
                        Type = module
                    },
                    new Permission
                    {
                        Id = id++,
                        DisplayName = $"{module} - View Assigned",
                        Code = $"{module}.ViewAssigned",
                        Description = "Allows viewing only assigned tickets",
                        Type = module
                    }
                });
                        }
                    }

            builder.HasData(permissions);


  
            #endregion


        }
    }
}
