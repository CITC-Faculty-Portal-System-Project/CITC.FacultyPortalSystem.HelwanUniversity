using Domain.Entities.AcademicDataModule.WritingsAndPatents;

namespace Presistence.Data.Configurations.WritingsAndPatentsModuleConfigurations
{
    public class ScientificWritingsConfigurations : IEntityTypeConfiguration<ScientificWritings>
    {
        public void Configure(EntityTypeBuilder<ScientificWritings> builder)
        {
            builder.ToTable("ScientificWritings");

            builder.HasKey(sw => sw.Id);

            builder.Property(sw => sw.Title)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(sw => sw.ISBN)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(sw => sw.PublishingHouse)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(sw => sw.Description)
                .HasMaxLength(500);

            builder.HasIndex(sw => sw.AuthorRoleId);    
            builder.HasIndex(sw => sw.Title);
            builder.HasIndex(sw => sw.ISBN);
            builder.HasIndex(sw => sw.PublishingHouse);
            builder.HasIndex(sw => sw.PublishingDate);

            #region Dropdown Relationship
            builder.HasOne(sw => sw.AuthorRole)
                .WithMany()
                .HasForeignKey(sw => sw.AuthorRoleId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region FacultyMember Relationship
            builder.HasOne(sw => sw.FacultyMember)
                .WithMany(fm => fm.ScientificWritings)
                .HasForeignKey(sw => sw.FacultyMemberId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
