namespace Domain.Entities.AcademicDataModule.ResearchesModule
{
    public class InternalSystemResearchContributor : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public bool IsFromHelwanUniversity { get; set; }
        public bool IsTheMajorResearcher { get; set; }
        public ICollection<InternalSystemResearchContributorsResearches>? Researches { get; set; }
    }
}
