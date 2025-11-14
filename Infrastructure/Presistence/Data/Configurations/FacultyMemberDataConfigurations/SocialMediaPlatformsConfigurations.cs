
namespace Presistence.Data.Configurations.FacultyMemberDataConfigurations
{
    public class SocialMediaPlatformsConfigurations : IEntityTypeConfiguration<SocialMediaPlatforms>
    {
        public void Configure(EntityTypeBuilder<SocialMediaPlatforms> builder)
        {
            builder.Property(sm => sm.LinkedIn)
               .HasColumnType("NVARCHAR(Max)")
               .IsRequired(false);
            builder.Property(sm => sm.Instagram)
                .HasColumnType("NVARCHAR(Max)")
                .IsRequired(false);
            builder.Property(sm => sm.PersonalWebsite)
                .HasColumnType("NVARCHAR(Max)")
                .IsRequired(false);
            builder.Property(sm => sm.GoogleScholar)
                .HasColumnType("NVARCHAR(Max)")
                .IsRequired(false);
            builder.Property(sm => sm.Scopus)
                .HasColumnType("NVARCHAR(Max)")
                .IsRequired(false);
            builder.Property(sm => sm.Facebook)
                .HasColumnType("NVARCHAR(Max)")
                .IsRequired(false);
            builder.Property(sm => sm.X)
                .HasColumnType("NVARCHAR(Max)")
                .IsRequired(false);
            builder.Property(sm => sm.YouTube)
                .HasColumnType("NVARCHAR(Max)")
                .IsRequired(false);
            builder.ToTable("SocialMedia");

            #region Relation With FacultyMember
            builder.HasOne(sm => sm.FacultyMember)
               .WithOne(fm => fm.SocialMediaPlatforms)
               .HasForeignKey<SocialMediaPlatforms>(sm => sm.FacultyMemberId)
               .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
