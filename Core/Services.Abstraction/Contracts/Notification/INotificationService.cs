using Shared.Dtos.Notification;

namespace Services.Abstraction.Contracts.Notification
{
    public interface INotificationService
    {
        public Task SendNotificationAsync(NotificationDTO notification);
        public Task MarkAsViewedAsync(Guid notificationId);
        public Task<bool> RemoveNotificationAsync(Guid notificationId);
        public Task<IEnumerable<NotificationDTO>> GetUserNotificationsAsync(Guid userId);
        public Task<Guid?> GetUnViewedNotificationId(string source, Guid receiverId); 
	}
}
