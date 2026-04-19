using Shared.Dtos;

namespace Services.Abstraction.Contracts
{
	public interface INotificationService
    {
		public Task SendNotificationAsync(NotificationDTO notification);
		public Task MarkAsViewedAsync(Guid notificationId);
		public Task RemoveNotificationAsync(Guid notificationId);
	}
}
