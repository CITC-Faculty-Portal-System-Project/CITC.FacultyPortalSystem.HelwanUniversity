using Domain.Entities.HigherStuidesModule;

namespace Presistence.Data.Configurations.HigherStuidesModuleConfigurations
{
    public class ThesesConfiguration : IEntityTypeConfiguration<Thesis>
    {
        public void Configure(EntityTypeBuilder<Thesis> builder)
        {

            #region ConfiguringProperties

            builder.Property(t => t.Link)
                    .HasMaxLength(500);

            builder.Property(t => t.Title)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(th => th.Type)
              .HasConversion<string>();

            #endregion

            #region ConfiguringRelations

            builder.HasOne(th => th.FacultyMember)
                   .WithMany(th => th.Theses)
                   .HasForeignKey(th => th.FacultyMemberId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(th => th.Grade)
                   .WithMany()
                   .HasForeignKey(th => th.GradeId)
                   .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region AddingIndcies

            builder.HasIndex(th => th.Title);
            
            #endregion
        
        }
    }
}
