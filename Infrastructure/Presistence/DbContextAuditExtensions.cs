using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace Presistence.Identity
{
    public static class DbContextAuditExtensions
    {
        public static void ApplyAuditConventions(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var rowVersion = entityType.FindProperty("RowVersion");
                if (rowVersion != null)
                {
                    rowVersion.IsConcurrencyToken = true;
                    rowVersion.ValueGenerated = ValueGenerated.OnAddOrUpdate;
                }
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IAuditablFields).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(DbContextAuditExtensions)
                        .GetMethod(nameof(AddSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)!
                        .MakeGenericMethod(entityType.ClrType);

                    method.Invoke(null, new object[] { modelBuilder });
                }
            }
        }

        private static void AddSoftDeleteFilter<TEntity>(ModelBuilder builder)
            where TEntity : class, IAuditablFields
            => builder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);

        public static void UpdateAuditFields(this DbContext dbContext)
        {
            var entries = dbContext.ChangeTracker.Entries<IAuditablFields>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.VersionNo += 1;
                    entry.Entity.UpdatedBy ??= "System";
                }

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy ??= "System";
                    entry.Entity.UpdatedBy ??= "System";
                    entry.Entity.VersionNo = 1;
                }

                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedBy ??= "System";
                    entry.Entity.DeletedAt = DateTime.UtcNow;
                    entry.Entity.VersionNo += 1;
                }
            }
        }
    }
}
