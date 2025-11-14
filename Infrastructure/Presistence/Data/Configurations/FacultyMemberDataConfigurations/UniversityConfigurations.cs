
namespace Presistence.Data.Configurations.FacultyMemberDataConfigurations
{
    public class UniversityConfigurations : IEntityTypeConfiguration<University>
    {
        public void Configure(EntityTypeBuilder<University> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UniversityName).HasMaxLength(50);
            builder.HasIndex(u => u.UniversityName).IsUnique();
        }
    }
}
