
namespace Presistence.Data.Configurations.FacultyMemberDataConfigurations
{
    public class ContactDataConfigurations : IEntityTypeConfiguration<ContactData>
    {
        public void Configure(EntityTypeBuilder<ContactData> builder)
        {
            builder.HasIndex(c => c.MainPhoneNumber);

            builder.HasIndex(c => c.OfficialEmail);

            builder.Property(cd => cd.Address)
                .HasColumnType("NVARCHAR(75)");

            builder.Property(cd => cd.AlternativeEmail)
                .HasColumnType("NVARCHAR(150)");

            builder.Property(cd => cd.OfficialEmail)
                .HasColumnType("NVARCHAR(150)");

            builder.Property(cd => cd.HomePhoneNumber)
                .HasColumnType("NVARCHAR(50)");

            builder.Property(cd => cd.MainPhoneNumber)
               .HasColumnType("NVARCHAR(50)");

            builder.Property(cd => cd.WorkPhoneNumber)
                .HasColumnType("NVARCHAR(50)");

            builder.Property(cd => cd.FaxNumber)
                .HasColumnType("NVARCHAR(150)");

            #region Relation With FacultyMember
            builder.HasOne(cd => cd.FacultyMember)
               .WithOne(fm => fm.ContactData)
               .HasForeignKey<ContactData>(cd => cd.FacultyMemberId)
               .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
