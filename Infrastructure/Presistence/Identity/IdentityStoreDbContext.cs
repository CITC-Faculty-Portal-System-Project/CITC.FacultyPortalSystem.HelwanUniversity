using Domain.Entities.IdentityModule.Authorization;
using Domain.Entities.IdentityModule.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Presistence.Identity
{
    public class IdentityStoreDbContext(DbContextOptions<IdentityStoreDbContext> options) : IdentityDbContext<User, Role, Guid>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(IdentityAssemblyMarker).Assembly);

            var keepNamespacePrefix = "Domain.Entities.IdentityModule";

            var identityAssemblies = new[]
            {
                typeof(IdentityUser<>).Assembly, 
                typeof(IdentityDbContext).Assembly 
            };

            foreach (var entityType in builder.Model.GetEntityTypes().ToList())
            {
                var clr = entityType.ClrType;

                var keep =
                    (clr.Namespace?.StartsWith(keepNamespacePrefix) == true)
                    || identityAssemblies.Contains(clr.Assembly);

                if (!keep)
                    builder.Ignore(clr);
            }

            builder.ApplyAuditConventions();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.UpdateAuditFields();
            return await base.SaveChangesAsync(cancellationToken);
        }


        #region DbSets

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolesPermissions { get; set; }
        public DbSet<UserPermission> UsersPermissions { get; set; }

        #endregion

    }
}
