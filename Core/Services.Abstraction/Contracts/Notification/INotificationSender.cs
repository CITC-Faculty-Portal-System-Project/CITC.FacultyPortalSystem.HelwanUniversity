using Shared.Dtos.Notification;
using Shared.Enums.NotificationModule;

namespace Services.Abstraction.Contracts.Notification
{
    public interface INotificationSender
	{
		NotificationType Type { get; }
		public Task SendAsync(NotificationDTO notification);
	}
}
