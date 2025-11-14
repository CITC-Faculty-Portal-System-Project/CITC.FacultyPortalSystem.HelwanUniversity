
namespace Presistence.Data.Configurations.ResearchesModuleConfigurations.Theses_Supervision
{
    public class ThesesSupervisionConfigurations : IEntityTypeConfiguration<ThesesSupervision>
    {
        public void Configure(EntityTypeBuilder<ThesesSupervision> builder)
        {
            builder.Property(SU => SU.Role)
                .HasColumnType("NVARCHAR(50)")
                .HasMaxLength(50);
            builder.Property(SU => SU.RegistrationDate)
                .HasColumnType("DATE");
            builder.Property(SU => SU.SupervisionFormationDate)
                .HasColumnType("DATE");
            builder.Property(SU => SU.DiscussionDate)
                .HasColumnType("DATE");
            builder.Property(SU => SU.GrantingDate)
                .HasColumnType("DATE");

            #region Relation With Theses
            builder.HasOne(su => su.Theses)
               .WithMany(t => t.Supervisions)
               .HasForeignKey(su => su.ThesesId)
               .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Relation WIth ThesesSupervisor
            builder.HasOne(su => su.ThesesSupervisor)
                .WithMany(s => s.ThesesSupervisions)
                .HasForeignKey(su => su.ThesesSupervisorId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion


        }
    }
}
