
using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Presistence.Data.Configurations.ProjectsAndCommitteesModuleConfigurations
{
    public class ProjectsConfigurations : IEntityTypeConfiguration<Projects>
    {
        public void Configure(EntityTypeBuilder<Projects> builder)
        {
            builder.ToTable("Projects");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.LocalOrInternational)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(p => p.NameOfProject)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(p => p.FinancingAuthority)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.HasIndex(p => p.NameOfProject);
            builder.HasIndex(p => p.StartDate);


            #region Dropdown Relationships
            builder.HasOne(p => p.TypeOfProject)
                   .WithMany()
                   .HasForeignKey(p => p.TypeOfProjectId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.ParticipationRole)
                   .WithMany()
                   .HasForeignKey(p => p.ParticipationRoleId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Relationship with Faculty Member
            builder.HasOne(p => p.FacultyMember)
                   .WithMany(f => f.Projects)
                   .HasForeignKey(p => p.FacultyMemberId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
