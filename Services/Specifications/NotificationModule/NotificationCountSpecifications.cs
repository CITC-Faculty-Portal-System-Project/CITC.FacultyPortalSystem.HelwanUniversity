using Shared.SpecificationParameters.NotificationsModule;

namespace Services.Specifications.NotificationModule
{
    internal class NotificationCountSpecifications : BaseSpecifications<Notification, Guid>
    {
        public NotificationCountSpecifications(NotificationSpecificationsParameters parameters)
            : base(n => !n.IsDeleted &&
                   !n.IsRemoved &&
                   n.ReceiverId == parameters.ReceiverId &&
                   (string.IsNullOrEmpty(parameters.Search) ||
                   n.Title.Contains(parameters.Search))
            )
        {
        }
    }
}
