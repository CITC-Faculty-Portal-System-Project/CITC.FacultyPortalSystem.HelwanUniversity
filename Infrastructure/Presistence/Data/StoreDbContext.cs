
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace Presistence.Data
{
    public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);

            // RowVersion Convention
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var rowVersion = entityType.FindProperty("RowVersion");
                if (rowVersion != null)
                {
                    rowVersion.IsConcurrencyToken = true;
                    rowVersion.ValueGenerated = ValueGenerated.OnAddOrUpdate;
                }
            }

            // Soft Delete Filters
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IAuditablFields).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(StoreDbContext)
                        .GetMethod(nameof(AddSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)!
                        .MakeGenericMethod(entityType.ClrType);

                    method.Invoke(null, new object[] { modelBuilder });
                }
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateAuditFields();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private static void AddSoftDeleteFilter<TEntity>(ModelBuilder builder) where TEntity : class, IAuditablFields
        {
            builder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
        }


        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries<IAuditablFields>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.Now;
                    entry.Entity.VersionNo += 1;
                    entry.Entity.UpdatedBy ??= "System";
                }
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.UpdatedAt = DateTime.Now;
                    entry.Entity.CreatedBy ??= "System";
                    entry.Entity.UpdatedBy ??= "System";
                    entry.Entity.VersionNo = 1;
                }
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedBy = "System";
                    entry.Entity.DeletedAt = DateTime.Now;
                    entry.Entity.VersionNo += 1;
                }
            }
        }
        #region FacultyMemberData DbSets
        public DbSet<ContactData> ContactData { get; set; }
        public DbSet<PersonalData> PersonalData { get; set; }
        public DbSet<IdentificationCard> IdentificationCard { get; set; }
        public DbSet<SocialMediaPlatforms> SocialMediaPlatforms { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<FieldOfStudy> FieldOfStudies { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<FacultyMember> FacultyMembers { get; set; }

        #endregion

        #region ResearchesModule DbSets
        public DbSet<Theses> Theses { get; set; }
        public DbSet<ThesesSupervision> ThesesSupervisions { get; set; }
        public DbSet<ThesesSupervisor> ThesesSupervisors { get; set; }
        #endregion
    }
}
