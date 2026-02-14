namespace Domain.Entities.AcademicDataModule.ResearchesModule
{
    public class ResearcherInterest : BaseEntity<int>
    {
        public int ResearcherId { get; set; }
        public ResearcherProfile? Researcher { get; set; }

        public int InterestId { get; set; }
        public ScientificInterest? Interest { get; set; }

    }
}
