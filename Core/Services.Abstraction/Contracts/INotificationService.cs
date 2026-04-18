using Shared.Dtos;

namespace Services.Abstraction.Contracts
{
	public interface INotificationService
    {
		public Task SendNotificationAsync(NotificationDto notification);
		public Task MarkAsViewedAsync(Guid notificationId);
		public Task RemoveNotificationAsync(Guid notificationId);
	}
}
