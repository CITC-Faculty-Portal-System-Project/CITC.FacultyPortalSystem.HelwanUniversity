using Shared.Enums.ResearchesModule;

namespace Shared.SpecificationParameters.ResearchesModule
{
    public class ResearchCursoredPaginationSpecificationParameters
    {
        public int? BeforeResearchId;
        public Guid FacultyMemberId { get; set; }
        public ResearchSource? Source { get; set; }
        public ResearchDerivedFrom? DerivedFrom { get; set; }
        public PublisherType? PublisherType { get; set; }
        public PublicationType? PublicationType { get; set; }
        public ResearchesSortingOptions Sort { get; set; }
        public string? Search { get; set; }
        private const int MaxTake = 50;
        private int take = 20;

        public int Take
        {
            get => take;
            set => take = value > MaxTake ? MaxTake : value;
        }



    }
}
