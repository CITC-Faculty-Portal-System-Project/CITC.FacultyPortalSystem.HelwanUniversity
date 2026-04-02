using Domain.Entities.AcademicDataModule.HigherStuidesModule;

namespace Presistence.Data.Configurations.HigherStuidesModuleConfigurations
{
    public class ThesisComitteeConfiguration : IEntityTypeConfiguration<ThesisComittee>
    {
        public void Configure(EntityTypeBuilder<ThesisComittee> builder)
        {

            #region ConfigruingProperties

            builder.Property(s => s.Role)
                   .HasConversion<string>();

            builder.Property(s => s.Name)
                 .HasMaxLength(250)
                 .IsRequired();

            builder.Property(s => s.Authority)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(t => t.MemberId)
                  .IsRequired(false);


            #endregion

            #region ConfiguringRelations

            builder.HasOne(s => s.JobLevel)
                  .WithMany()
                  .HasForeignKey(s => s.JobLevelId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ss => ss.Theses)
              .WithMany(t => t.ComitteeMembers)
              .HasForeignKey(ss => ss.ThesesId)
              .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(ss => ss.Member)
              .WithMany(t => t.ThesisComittees)
              .HasForeignKey(ss => ss.MemberId)
              .OnDelete(DeleteBehavior.Restrict);


            #endregion

            #region AddingIndecies

            builder.HasIndex(s => s.Name);

            #endregion

        }
    }
}
