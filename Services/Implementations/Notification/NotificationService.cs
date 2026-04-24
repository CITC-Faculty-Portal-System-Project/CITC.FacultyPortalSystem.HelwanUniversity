using Services.Abstraction.Contracts.Notification;
using Services.Global;
using Services.Specifications.NotificationModule;
using Shared.Dtos.Notification;
using Shared.Enums.NotificationModule;

namespace Services.Implementations.Notification
{
    public class NotificationService(IUnitOfWork _unitOfWork,
		IAuthenticationService _authentication,
		IMapper _mapper,
		IEnumerable<INotificationSender> _senders) :BaseService<Domain.Entities.Notification,Guid> (_unitOfWork,_authentication,_mapper),
		INotificationService
	{
		protected override string EntityName => "Notification";

		public async Task<Guid?> GetUnViewedNotificationId(string source, Guid receiverId)
		{
			var notification = await Repo.GetAsync(new NotificationSpecifications(source, receiverId));
			if(notification is not null && !notification.IsDeleted)
					return notification.Id;
			return null;
		}

		public Task<IEnumerable<NotificationDTO>> GetUserNotificationsAsync(Guid userId)
		{
			throw new NotImplementedException();
		}

		public async Task MarkAsViewedAsync(Guid notificationId)
		{
			var notification = await Repo.GetAsync(new NotificationSpecifications(notificationId))
				?? throw NotFound();

			notification.IsViewed = true;
			notification.IsRemoved = true;
			notification.IsDeleted = true;

			Repo.Update(notification);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task<bool> RemoveNotificationAsync(Guid notificationId)
		{
			var notification = await Repo.GetAsync(new NotificationSpecifications(notificationId))
				?? throw NotFound();

			if (notification.Type == NotificationType.SystemAlert)
				return false;

			notification.IsRemoved = true;
			Repo.Update(notification);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		public async Task SendNotificationAsync(NotificationDTO notification)
		{
			var sender = _senders.FirstOrDefault(x => x.Type == notification.Type)
				?? throw NotFound();

			await sender.SendAsync(notification);
		}
	}
}
