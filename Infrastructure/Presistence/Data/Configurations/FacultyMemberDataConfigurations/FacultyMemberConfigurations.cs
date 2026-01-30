
using Domain.Entities.Attachments;

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


            #region Configuring Relation With Attachments

            builder.HasMany(f => f.Attachments)
                .WithOne(a => a.FacultyMember)
                .HasForeignKey(a => a.FacultyMemberId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}
