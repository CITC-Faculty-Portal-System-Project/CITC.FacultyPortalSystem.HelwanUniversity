namespace Domain.Entities.AcademicDataModule.ResearchesModule
{
    public class InternalSystemResearchContributorsResearches : BaseEntity<int>
    {
        public int InternalSystemResearchId { get; set; }
        public InternalSystemResearch? InternalSystemResearch { get; set; }

        public int InternalSystemResearchContributorId { get; set; }
        public InternalSystemResearchContributor? InternalSystemResearchContributor { get; set; }


    }
}
