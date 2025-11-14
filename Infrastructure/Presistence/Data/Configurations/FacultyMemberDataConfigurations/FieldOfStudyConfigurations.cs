
namespace Presistence.Data.Configurations.FacultyMemberDataConfigurations
{
    public class FieldOfStudyConfigurations : IEntityTypeConfiguration<FieldOfStudy>
    {
        public void Configure(EntityTypeBuilder<FieldOfStudy> builder)
        {
            builder.Property(f => f.FieldOfStudyName)
                .IsRequired()
                .HasMaxLength(100);

            #region Relation With Faculty
            builder.HasOne(f => f.Faculty)
                .WithMany(fac => fac.FieldsOfStudy)
                .HasForeignKey(f => f.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
