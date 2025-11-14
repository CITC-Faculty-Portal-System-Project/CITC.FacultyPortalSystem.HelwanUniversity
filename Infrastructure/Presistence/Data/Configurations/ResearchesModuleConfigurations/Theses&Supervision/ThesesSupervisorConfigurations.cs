
namespace Presistence.Data.Configurations.ResearchesModuleConfigurations.Theses_Supervision
{
    public class ThesesSupervisorConfigurations : IEntityTypeConfiguration<ThesesSupervisor>
    {
        public void Configure(EntityTypeBuilder<ThesesSupervisor> builder)
        {
            builder.Property(S => S.Name)
                .HasColumnType("NVARCHAR(50)")
                .HasMaxLength(50);
            builder.HasIndex(S => S.Name);
            builder.Property(S => S.JobLevel)
                .HasColumnType("NVARCHAR(50)")
                .HasMaxLength(50);
            builder.Property(S => S.Authority)
                .HasColumnType("NVARCHAR(50)")
                .HasMaxLength(50);

            #region Relation With ThesesSupervision
            builder.HasMany(S => S.ThesesSupervisions)
                .WithOne(Su => Su.ThesesSupervisor)
                .HasForeignKey(Su => Su.ThesesSupervisorId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Relation With FacultyMember
            builder.HasOne(s => s.FacultyMember)
                .WithMany()
                .HasForeignKey(s => s.FacultyMemberId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion
        }
    }
}
