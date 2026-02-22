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


            #region Configuring Relation With Researches

            builder.HasMany(f => f.ResearchContributions)
                    .WithOne(a => a.Contributor)
                    .HasForeignKey(a => a.ContributorId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(f => f.Theses)
                .WithOne(th => th.FacultyMember)
                .HasForeignKey(th => th.FacultyMemberId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(f=> f.ThesisComittees)
                .WithOne(tc => tc.Member)
                .HasForeignKey(tc => tc.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}
