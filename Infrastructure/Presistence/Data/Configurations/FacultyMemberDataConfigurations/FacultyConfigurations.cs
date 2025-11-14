
namespace Presistence.Data.Configurations.FacultyMemberDataConfigurations
{
    public class FacultyConfigurations : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> builder)
        {
            builder.Property(f => f.FacultyName)
                .HasMaxLength(100);

            #region Relation With University
            builder.HasOne(f => f.University)
                .WithMany(u => u.Faculties)
                .HasForeignKey(f => f.UniversityId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
