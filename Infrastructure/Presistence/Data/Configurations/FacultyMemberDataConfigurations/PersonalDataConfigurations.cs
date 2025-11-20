using Domain.Entities.FacultyMemberDataModule;

namespace Presistence.Data.Configurations.FacultyMemberDataConfigurations
{
    public class PersonalDataConfigurations : IEntityTypeConfiguration<PersonalData>
    {
        public void Configure(EntityTypeBuilder<PersonalData> builder)
        {
            builder.Property(pd => pd.Name)
                .HasMaxLength(50);

            builder.Property(pd => pd.BirthDate)
                .HasColumnType("Date");

            builder.Property(pd => pd.BirthPlace)
                .HasMaxLength(50);

            builder.Property(pd => pd.NameInComposition)
                .HasMaxLength(50);

            builder.Property(pd => pd.CompositionTopics)
                .HasColumnType("NVARCHAR(Max)");

            builder.Property(pd => pd.GeneralSpecialization)
                .HasMaxLength(250);

            builder.Property(pd => pd.AccurateSpecialization)
                .HasMaxLength(250);

            #region Dropdown Relationships
            builder.HasOne(pd => pd.Title)
                   .WithMany()
                   .HasForeignKey(pd => pd.TitleId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pd => pd.Gender)
                   .WithMany()
                   .HasForeignKey(pd => pd.GenderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pd => pd.MaritalStatus)
                   .WithMany()
                   .HasForeignKey(pd => pd.MaritalStatusId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pd => pd.University)
                   .WithMany()
                   .HasForeignKey(pd => pd.UniversityId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pd => pd.Department)
                   .WithMany()
                   .HasForeignKey(pd => pd.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pd => pd.Authority)
                   .WithMany()
                   .HasForeignKey(pd => pd.AuthorityId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pd => pd.Field)
                   .WithMany()
                   .HasForeignKey(pd => pd.FieldId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Relation With FacultyMember
            builder.HasOne(pd => pd.FacultyMember)
               .WithOne(fm => fm.PersonalData)
               .HasForeignKey<PersonalData>(pd => pd.FacultyMemberId)
               .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
