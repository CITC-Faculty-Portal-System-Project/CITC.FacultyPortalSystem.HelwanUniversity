using Shared.Enums.ResearchesModule;

namespace Shared.SpecificationParameters.ResearchesModule
{
    public class ResearchSpecificationParameters 
    {
        private const int defaultPageSize = 9;
        private const int maxPageSize = 9;
        public Guid FacultyMemberId { get; set; }
        public ResearchSource? Source { get; set; }
        public ResearchDerivedFrom? DerivedFrom { get; set; }
        public PublisherType? PublisherType { get; set; }
        public PublicationType? PublicationType { get; set; }
        public ResearchesSortingOptions Sort { get; set; }
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;
        private int _pageSize = defaultPageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }

    }
}
