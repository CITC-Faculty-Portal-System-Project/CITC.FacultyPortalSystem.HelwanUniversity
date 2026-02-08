namespace Domain.Entities.AcademicDataModule.ResearchesModule
{
    public class ResearchContribution : BaseEntity<int>
    {
        public string MemberAcademicName { get; set; } = string.Empty;
        public ContributorType ContributorType { get; set; }
        public bool IsTheMajorResearcher { get; set; }
        public string ContributorOrgansationId { get; set; } = string.Empty;
        public bool IsConfirmed { get; set; }

        public int ResearchId { get; set; }
        public Research? Research { get; set; }

        public Guid? ContributorId { get; set; }
        public FacultyMember? Contributor { get; set; }

    }
}
