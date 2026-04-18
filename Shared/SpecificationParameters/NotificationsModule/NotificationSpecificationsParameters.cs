using Shared.Enums.AcademicDataModule.ContributionsModule;

namespace Shared.SpecificationParameters.NotificationsModule
{
    public class NotificationSpecificationsParameters
    {
        private const int defaultPageSize = 10;
        private const int maxPageSize = 10;
        public Guid ReceiverId { get; set; } 
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
