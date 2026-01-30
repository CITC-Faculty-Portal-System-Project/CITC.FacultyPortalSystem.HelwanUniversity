namespace Presistence.Data.Configurations.ScientificProgressionModuleConfigurations
{
    public class AcademicQualificationsConfigurations : IEntityTypeConfiguration<AcademicQualifications>
    {
        public void Configure(EntityTypeBuilder<AcademicQualifications> builder)
        {
            builder.ToTable("AcademicQualifications");

            builder.HasKey(aq => aq.Id);

            builder.Property(aq => aq.Specialization)
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(aq  => aq.UniversityOrFaculty)
                   .HasMaxLength(250);

            builder.Property(aq => aq.CountryOrCity)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.HasIndex(aq => aq.CountryOrCity);
            builder.HasIndex(aq => aq.DateOfObtainingTheQualification);

            #region Dropdown Relationships
            builder.HasOne(aq => aq.Qualification)
                   .WithMany()
                   .HasForeignKey(aq => aq.QualificationId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(aq => aq.Grade)
                   .WithMany()
                   .HasForeignKey(aq => aq.GradeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(aq => aq.DispatchType)
                   .WithMany()
                   .HasForeignKey(aq => aq.DispatchId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Relationship with Faculty Member
            builder.HasOne(aq => aq.FacultyMember)
                   .WithMany(f => f.AcademicQualifications)
                   .HasForeignKey(aq => aq.FacultyMemberId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region RelationShip With Attachments

            builder.HasOne(aq => aq.Attachment)
             .WithOne(f => f.AcademicQualification)
             .HasForeignKey<AcademicQualifications>(aq => aq.AttachmentId)
             .OnDelete(DeleteBehavior.Cascade);

            #endregion

        }
    }
}
