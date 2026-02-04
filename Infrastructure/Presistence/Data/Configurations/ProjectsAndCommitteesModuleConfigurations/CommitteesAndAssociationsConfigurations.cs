using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Presistence.Data.Configurations.ProjectsAndCommitteesModuleConfigurations
{
    public class CommitteesAndAssociationsConfigurations : IEntityTypeConfiguration<CommitteesAndAssociations>
    {
        public void Configure(EntityTypeBuilder<CommitteesAndAssociations> builder)
        {
            builder.ToTable("CommitteesAndAssociations", t => t.HasCheckConstraint("CK_CommitteesAndAssociations_Dates", "[EndDate] >= [StartDate]"));

            builder.HasKey(caa => caa.Id);

            builder.Property(caa => caa.NameOfCommitteeOrAssociation)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(caa => caa.Notes)
                .HasMaxLength(500);

            builder.HasIndex(caa => caa.NameOfCommitteeOrAssociation);
            builder.HasIndex(caa => caa.StartDate);

            #region Dropdown Relationships
            builder.HasOne(caa => caa.TypeOfCommitteeOrAssociation)
                   .WithMany()
                   .HasForeignKey(caa => caa.TypeOfCommitteeOrAssociationId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(caa => caa.DegreeOfSubscription)
                   .WithMany()
                   .HasForeignKey(caa => caa.DegreeOfSubscriptionId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Relationship with Faculty Member
            builder.HasOne(caa => caa.FacultyMember)
                   .WithMany(f => f.CommitteesAndAssociations)
                   .HasForeignKey(caa => caa.FacultyMemberId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
