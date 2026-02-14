using Domain.Entities.AcademicDataModule.PrizesModule;

namespace Presistence.Data.Configurations.PrizesModuleConfigurations
{
    public class PrizesAndRewardsConfigurations : IEntityTypeConfiguration<PrizesAndRewards>
    {
        public void Configure(EntityTypeBuilder<PrizesAndRewards> builder)
        {
            builder.ToTable("PrizesAndRewards");

            builder.HasKey(par => par.Id);

            builder.Property(par => par.AwardingAuthority)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(par => par.Description)
                .HasMaxLength(500);

            builder.HasIndex(par => par.AwardingAuthority);
            builder.HasIndex(par => par.DateReceived);
            builder.HasIndex(par => par.PrizeId);

            #region Dropdown Reationship
            builder.HasOne(par => par.Prize)
                .WithMany()
                .HasForeignKey(par => par.PrizeId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region FacultyMember Relationship
            builder.HasOne(par => par.FacultyMember)
                .WithMany(fm => fm.PrizesAndRewards)
                .HasForeignKey(par => par.FacultyMemberId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
