using Shared.SpecificationParameters.NotificationsModule;
using System.Linq.Expressions;

namespace Services.Specifications.NotificationModule
{
    internal class NotificationSpecifications : BaseSpecifications<Notification, Guid>
    {
        public NotificationSpecifications(NotificationSpecificationsParameters parameters) 
            : base(n => !n.IsDeleted && 
                   !n.IsRemoved &&
                   n.ReceiverId == parameters.ReceiverId &&
                   (string.IsNullOrEmpty(parameters.Search) ||
                   n.Title.Contains(parameters.Search))
            )
        {
            applyPagination(parameters.PageSize, parameters.PageIndex);
        }

        public NotificationSpecifications(Guid notificationId) : base(n => !n.IsDeleted && n.Id == notificationId)
        {

        }

        public NotificationSpecifications(string source, Guid receiverId) : base(n => n.Source == source && n.ReceiverId == receiverId)
        {

        }
    }
}
