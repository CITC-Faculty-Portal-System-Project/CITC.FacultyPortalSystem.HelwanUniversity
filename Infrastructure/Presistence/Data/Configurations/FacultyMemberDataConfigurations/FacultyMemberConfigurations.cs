
using Domain.Entities.ResearchesModule.Theses_Supervision;

namespace Presistence.Data.Configurations.FacultyMemberDataConfigurations
{
    public class FacultyMemberConfigurations : IEntityTypeConfiguration<FacultyMember>
    {
        public void Configure(EntityTypeBuilder<FacultyMember> builder)
        {
            builder.HasIndex(fm => fm.Email).IsUnique();

            builder.HasIndex(fm => fm.NationalNumber).IsUnique();

            builder.HasIndex(fm => fm.Name);

            builder.Property(fm => fm.NationalNumber)
                .HasColumnType("NVARCHAR(14)")
                .IsRequired();

            builder.Property(fm => fm.Name)
                .HasMaxLength(100);

            builder.Property(fm => fm.Email)
                .HasColumnType("NVARCHAR(150)");

        }
    }
}
