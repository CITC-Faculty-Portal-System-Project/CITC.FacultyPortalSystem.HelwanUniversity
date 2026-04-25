using Shared.Dtos.Notification;
using Shared.SpecificationParameters.NotificationsModule;

namespace Services.Abstraction.Contracts.Notification
{
    public interface INotificationService
    {
        public Task SendNotificationAsync(NotificationDTO notification);
        public Task MarkAsViewedAsync(Guid notificationId);
        public Task<bool> RemoveNotificationAsync(Guid notificationId);
        public Task<CursorPaginatedResult<NotificationDTO, Guid>> GetUserNotificationsAsync(NotificationSpecificationsParameters parameters);
        public Task<Guid?> GetUnViewedNotificationId(string source, Guid receiverId); 
	}
}
