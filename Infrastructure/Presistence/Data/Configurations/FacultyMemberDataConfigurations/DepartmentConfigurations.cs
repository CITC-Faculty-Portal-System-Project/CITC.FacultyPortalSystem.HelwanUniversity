
namespace Presistence.Data.Configurations.FacultyMemberDataConfigurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasIndex(d => new { d.FieldOfStudyId, d.DepartmentName }).IsUnique();

            builder.Property(d => d.DepartmentName)
                .HasMaxLength(100);

            #region Relation With FieldOfStudy
            builder.HasOne(d => d.FieldOfStudy)
                .WithMany(f => f.Departments)
                .HasForeignKey(d => d.FieldOfStudyId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
