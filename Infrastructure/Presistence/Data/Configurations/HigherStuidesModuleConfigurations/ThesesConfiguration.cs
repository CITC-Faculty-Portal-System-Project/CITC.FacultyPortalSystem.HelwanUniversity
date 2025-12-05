using Domain.Entities.HigherStuidesModule;

namespace Presistence.Data.Configurations.HigherStuidesModuleConfigurations
{
    public class ThesesConfiguration : IEntityTypeConfiguration<Thesis>
    {
        public void Configure(EntityTypeBuilder<Thesis> builder)
        {
            builder.HasOne(th => th.FacultyMember)
                   .WithMany()
                   .HasForeignKey(th => th.FacultyMemberId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(th => th.Grade)
                   .WithMany()
                   .HasForeignKey(th => th.GradeId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.Property(th => th.Type)
                   .HasConversion<string>();

            builder.HasIndex(th => th.Title);
        }
    }
}
