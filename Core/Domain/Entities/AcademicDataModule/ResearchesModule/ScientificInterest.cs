namespace Domain.Entities.AcademicDataModule.ResearchesModule
{
    public class ScientificInterest : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<ResearcherInterest>? Researchers { get; set; } = new List<ResearcherInterest>();
    }
}
