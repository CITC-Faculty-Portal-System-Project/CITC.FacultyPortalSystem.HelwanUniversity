using Domain.Entities.AcademicDataModule.HigherStuidesModule;

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
            
            
            builder.HasMany(th => th.ComitteeMembers)
              .WithOne(th => th.Theses)
              .HasForeignKey(th => th.ThesesId)
              .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(th => th.Grade)
                   .WithMany()
                   .HasForeignKey(th => th.GradeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(th => th.Attachments)
                 .WithOne(a => a.Thesis)
                 .HasForeignKey(a => a.ThesisId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(th => th.Supervisings)
                 .WithOne(s => s.Thesis)
                 .HasForeignKey(s => s.ThesisId)
                 .OnDelete(DeleteBehavior.Restrict);


            #endregion

            #region AddingIndcies

            builder.HasIndex(th => th.Title);
            builder.HasIndex(th => th.EnrollmentDate);
            builder.HasIndex(th => th.RegistrationDate);
            builder.HasIndex(th => th.GradeId);
            builder.HasIndex(th => th.Type);
            builder.HasIndex(th => th.UniversityOrFaculty);
            builder.HasIndex(th => th.DiscussionDate);
            
            #endregion
        
        }
    }
}
