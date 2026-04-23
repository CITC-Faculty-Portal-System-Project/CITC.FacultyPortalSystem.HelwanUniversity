using Microsoft.AspNetCore.SignalR;
using Services.Abstraction.Contracts.Notification;
using Services.Specifications.NotificationModule;
using Shared.Dtos.Notification;
using Shared.Enums.NotificationModule;
using Shared.Hubs;

namespace Services.Implementations.Notification
{
	public class SystemAlertNotification(IHubContext<NotificationHub> _hub,
		IUnitOfWork _unitOfWork,
		IMapper _mapper) : INotificationSender
	{
		public NotificationType Type => NotificationType.SystemAlert;
		private IGenericRepository<Domain.Entities.Notification, Guid> Repo => _unitOfWork.GetRepository<Domain.Entities.Notification, Guid>();
		public async Task SendAsync(NotificationDTO notification)
		{
			var notificationDB = await Repo.GetAsync(new NotificationSpecifications(notification.Source, notification.ReceiverId));

			if (notificationDB is null)
			{
				notificationDB = _mapper.Map<Domain.Entities.Notification>(notification);
				await Repo.AddAsync(notificationDB);
				await _unitOfWork.SaveChangesAsync();
				await _hub
						.Clients
						.User(notification.ReceiverId.ToString())
						.SendAsync("ReceiveNotification", _mapper.Map<NotificationDTO>(notificationDB));
			}
			else
			{
				_mapper.Map(notification, notificationDB);
				notificationDB = await UpdateAlertAsync(notificationDB);
				Repo.Update(notificationDB);
				await _unitOfWork.SaveChangesAsync();
			}
		}

		#region HELPER
		private async Task<Domain.Entities.Notification> UpdateAlertAsync(Domain.Entities.Notification notification)
		{
			//If didn't view the alert before just update the notification's time
			notification.UpdatedAt = DateTime.Now;
			if(notification.IsDeleted)
			{
				notification.IsViewed = false;
				notification.IsDeleted = false;
				notification.IsRemoved = false;
				await _hub
						.Clients
						.User(notification.ReceiverId.ToString())
						.SendAsync("ReceiveNotification", _mapper.Map<NotificationDTO>(notification));
			}
			return notification;
		}
		#endregion
	}
}
