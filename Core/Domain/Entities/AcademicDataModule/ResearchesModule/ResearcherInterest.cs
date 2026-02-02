namespace Domain.Entities.AcademicDataModule.ResearchesModule
{
    public class ResearcherInterest : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public int ResearcherId { get; set; }
        public Researcher? Researcher { get; set; }
    
    }
}
