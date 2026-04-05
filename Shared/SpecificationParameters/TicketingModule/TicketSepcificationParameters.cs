using Shared.Enums.ResearchesModule;
using Shared.Enums.TicketingModule;

namespace Shared.SpecificationParameters.TicketingModule
{
    public class TicketSepcificationParameters
    {
        private const int defaultPageSize = 9;
        private const int maxPageSize = 9;
        public Guid FacultyMemberId { get; set; }
        public TicketsSortingOptions Sort { get; set; }
        public TicketType? Type { get; set; }
        public TicketStatus? Status { get; set; }
        public TicketPriority? Priority { get; set; }
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
