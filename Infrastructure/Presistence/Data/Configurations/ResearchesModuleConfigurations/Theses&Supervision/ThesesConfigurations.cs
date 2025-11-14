
namespace Presistence.Data.Configurations.ResearchesModuleConfigurations.Theses_Supervision
{
    public class ThesesConfigurations : IEntityTypeConfiguration<Theses>
    {
        public void Configure(EntityTypeBuilder<Theses> builder)
        {
            builder.HasIndex(t => t.ThesesType);

            builder.HasIndex(t => t.EnrollmentDate);

            builder.Property(Th => Th.Title)
                .HasColumnType("NVARCHAR(MAX)");
            builder.Property(Th => Th.Grade)
                .HasColumnType("NVARCHAR(50)")
                .HasMaxLength(50);
            builder.Property(Th => Th.ThesesInEnglishHyperLink)
                .HasColumnType("NVARCHAR(MAX)");
            builder.Property(Th => Th.StudentName)
                .HasColumnType("NVARCHAR(100)")
                .HasMaxLength(100);
            builder.HasIndex(Th => Th.StudentName);
            builder.Property(Th => Th.StudentMajor)
                .HasColumnType("NVARCHAR(100)")
                .HasMaxLength(100);
            builder.Property(Th => Th.StudentNationalNumber)
                .HasColumnType("NVARCHAR(14)")
                .HasMaxLength(14);
            builder.HasIndex(Th => Th.StudentNationalNumber)
            .IsUnique();
            builder.Property(Th => Th.EnrollmentDate)
                .HasColumnType("DATE");
            builder.Property(Th => Th.RegistrationDate)
                .HasColumnType("DATE");
            builder.Property(Th => Th.InternalGradeDate)
                .HasColumnType("DATE");
            builder.Property(Th => Th.SupervisionConfirmationDate)
                .HasColumnType("DATE");
            builder.Property(Th => Th.ThesesType)
                .HasConversion((thesesType) => thesesType.ToString(),
                (type) => (ThesesType)Enum.Parse(typeof(ThesesType), type));

            #region Relation With ThesesSupervision
            builder.HasMany(Th => Th.Supervisions)
                .WithOne(S => S.Theses)
                .HasForeignKey(S => S.ThesesId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
