using Shared.Enums.AcademicDataModule.ContributionsModule;
using Shared.Enums.ResearchesModule;

namespace Shared.SpecificationParameters.NotificationsModule
{
    public class NotificationSpecificationsParameters
    {
        public Guid? BeforeNotificationId;
        public Guid? ReceiverId { get; set; }
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
