namespace Domain.Entities.AcademicDataModule.ResearchesModule
{
    public class ResearchCite : BaseEntity<int>
    {
        public int ResearchId { get; set; }
        public Research? Research { get; set; }

        public int Year { get; set; }
        public int NumberOfCites { get; set; }
    }
}
