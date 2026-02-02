
using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Presistence.Data.Configurations.ProjectsAndCommitteesModuleConfigurations
{
    public class ParticipationInMagazinesConfigurations : IEntityTypeConfiguration<ParticipationInMagazines>
    {
        public void Configure(EntityTypeBuilder<ParticipationInMagazines> builder)
        {
            builder.ToTable("ParticipationInMagazines");

            builder.HasKey(pim => pim.Id);

            builder.Property(pim => pim.NameOfMagazine)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(pim => pim.WebsiteOfMagazine)
                .HasColumnType("NVARCHAR(MAX)");

            builder.HasIndex(pim => pim.NameOfMagazine);

            #region Dropdown Relationships
            builder.HasOne(pim => pim.TypeOfParticipation)
                   .WithMany()
                   .HasForeignKey(pim => pim.TypeOfParticipationId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Relationship with Faculty Member
            builder.HasOne(pim => pim.FacultyMember)
                   .WithMany(f => f.ParticipationInMagazines)
                   .HasForeignKey(pim => pim.FacultyMemberId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
