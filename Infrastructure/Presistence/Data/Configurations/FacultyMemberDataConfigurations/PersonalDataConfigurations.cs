namespace Presistence.Data.Configurations.FacultyMemberDataConfigurations
{
    public class PersonalDataConfigurations : IEntityTypeConfiguration<PersonalData>
    {
        public void Configure(EntityTypeBuilder<PersonalData> builder)
        {
            builder.Property(p => p.Name)
                .HasMaxLength(50);

            builder.Property(p => p.Title)
                .HasMaxLength(50);

            builder.Property(p => p.Gender)
                .HasConversion((Gender) => Gender.ToString(),
                (type) => (Gender)Enum.Parse(typeof(Gender), type));

            builder.Property(p => p.SocialStatus)
                .HasMaxLength(20);

            builder.Property(p => p.BirthDate)
                .HasColumnType("Date");

            builder.Property(p => p.BirthPlace)
                .HasMaxLength(30);

            builder.Property(p => p.NameInComposition)
                .HasMaxLength(50);

            builder.Property(p => p.CompositionTopics)
                .HasColumnType("NVARCHAR(Max)");

            #region Relation With FacultyMember
            builder.HasOne(pd => pd.FacultyMember)
               .WithOne(fm => fm.PersonalData)
               .HasForeignKey<PersonalData>(pd => pd.FacultyMemberId)
               .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
