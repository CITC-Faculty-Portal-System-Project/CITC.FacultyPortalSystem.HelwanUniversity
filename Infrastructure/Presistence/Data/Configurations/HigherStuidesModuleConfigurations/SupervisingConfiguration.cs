using Domain.Entities.HigherStuidesModule;

namespace Presistence.Data.Configurations.HigherStuidesModuleConfigurations
{
    public class SupervisingConfiguration : IEntityTypeConfiguration<Supervising>
    {
        public void Configure(EntityTypeBuilder<Supervising> builder)
        {
            builder.HasOne<FacultyMember>()
                   .WithMany()
                   .HasForeignKey(s => s.FacultyMemberId)
                   .OnDelete(DeleteBehavior.Cascade);
                   

            builder.Property(s => s.FacultyMemberRole)
                   .HasConversion<string>();

            builder.Property(s => s.Type)
                   .HasConversion<string>();

            builder.HasOne(s => s.Grade)
                  .WithMany()
                  .HasForeignKey(s => s.GradeId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(s => s.Title);
            builder.HasIndex(s => s.StudentName);


        }
    }
}
