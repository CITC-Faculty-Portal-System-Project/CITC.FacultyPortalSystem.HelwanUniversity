using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Domain.Entities.AcademicDataModule.MissionsModule;
using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Domain.Entities.FacultyMemberDataModule;
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
        public DbSet<FacultyMember> FacultyMembers { get; set; }

        #endregion

        #region ScientificProgressionModule DbSets
        public DbSet<AcademicQualifications> AcademicQualifications { get; set; }
        public DbSet<JobRanks> JobRanks { get; set; }
        public DbSet<AdministrativePositions> AdministrativePositions { get; set; }
        #endregion

        #region MissionsModule DbSets
        public DbSet<ConferencesAndSeminars> ConferencesAndSeminars { get; set; }
        public DbSet<ScientificMissions> ScientificMissions { get; set; }
        public DbSet<TrainingPrograms> TrainingPrograms { get; set; }
        #endregion

        #region ProjectsAndCommitteesModule DbSets
        public DbSet<CommitteesAndAssociations> CommitteesAndAssociations { get; set; }
        public DbSet<ReviewingArticles> ReviewingArticles { get; set; }
        public DbSet<ParticipationInMagazines> ParticipationInMagazines { get; set; }
        public DbSet<Projects> Projects { get; set; }
        #endregion

        #region ResearchModule DbSets

        public DbSet<ResearcherProfile> ResearchersProfiles { get; set; }
        public DbSet<ResearchContribution> ResearchesContributions { get; set; }
        public DbSet<ResearcherInterest> ResearchersInterests { get; set; }
        public DbSet<Research> Researches { get; set; }
        public DbSet<ResearcherCite> ResearchersCites { get; set; }
        public DbSet<ResearchCite> ResearchsCites { get; set; }
        public DbSet<ScientificInterest> ScientificInterests { get; set; }
        public DbSet<ResearchAttachment> ResearchAttachments { get; set; }

        #endregion

        #region HigherStudiesModule DbSets

        public DbSet<Supervising> Supervisings { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Thesis> Theses { get; set; }
        public DbSet<ThesesAttachment> ThesesAttachments { get; set; }

        #endregion

        public DbSet<Lookup> Lookups { get; set; }

     }
}
