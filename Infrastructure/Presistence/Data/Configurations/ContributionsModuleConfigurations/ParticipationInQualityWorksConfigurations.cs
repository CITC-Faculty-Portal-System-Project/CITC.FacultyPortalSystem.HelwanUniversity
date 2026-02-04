using Domain.Entities.AcademicDataModule.ContributionsModule;

namespace Presistence.Data.Configurations.ContributionsModuleConfigurations
{
    public class ParticipationInQualityWorksConfigurations : IEntityTypeConfiguration<ParticipationInQualityWorks>
    {
        public void Configure(EntityTypeBuilder<ParticipationInQualityWorks> builder)
        {
            builder.ToTable("ParticipationInQualityWorks", piqw => piqw.HasCheckConstraint("CK_ParticipationInQualityWorks_Dates", "[EndDate] >= [StartDate]"));

            builder.HasKey(piqw => piqw.Id);

            builder.Property(piqw => piqw.ParticipationTitle)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(piqw => piqw.Description)
                .HasMaxLength(500);

            builder.HasIndex(piqw => piqw.StartDate);
            builder.HasIndex(piqw => piqw.ParticipationTitle);

            #region FacultyMember Relationship
            builder.HasOne(piqw => piqw.FacultyMember)
                .WithMany(fm => fm.ParticipationInQualityWorks)
                .HasForeignKey(piqw => piqw.FacultyMemberId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
