
namespace Presistence.Data.Configurations.FacultyMemberDataConfigurations
{
    public class SpecializationConfigurations : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasIndex(s => new { s.DepartmentId, s.GeneralSpecialization });

            builder.Property(s => s.GeneralSpecialization)
                .HasMaxLength(100);

            builder.Property(s => s.AccurateSpecialization)
                .HasMaxLength(100);

            #region Relations With Department
            builder.HasOne(s => s.Department)
                .WithMany(d => d.Specializations)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
