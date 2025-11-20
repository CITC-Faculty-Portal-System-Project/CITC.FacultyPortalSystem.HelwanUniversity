
namespace Presistence.Data.Configurations.ProjectsAndCommitteesModuleConfigurations
{
    public class ReviewingArticlesConfigurations : IEntityTypeConfiguration<ReviewingArticles>
    {
        public void Configure(EntityTypeBuilder<ReviewingArticles> builder)
        {
            builder.ToTable("ReviewingArticles");

            builder.HasKey(ra => ra.Id);

            builder.Property(ra => ra.TitleOfArticle)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(ra => ra.Authority)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(ra => ra.Description)
                .HasMaxLength(500);

            builder.HasIndex(ra => ra.ReviewingDate);
            builder.HasIndex(ra => ra.TitleOfArticle);

            #region Relationship with Faculty Member
            builder.HasOne(ra => ra.FacultyMember)
                   .WithMany(f => f.ReviewingArticles)
                   .HasForeignKey(ra => ra.FacultyMemberId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
