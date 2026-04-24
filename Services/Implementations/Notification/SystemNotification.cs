using Microsoft.AspNetCore.SignalR;
using Services.Abstraction.Contracts.Notification;
using Shared.Dtos.Notification;
using Shared.Enums.NotificationModule;
using Shared.Hubs;

namespace Services.Implementations.Notification
{
	public class SystemNotification(IHubContext<NotificationHub> _hub,
		IUnitOfWork _unitOfWork,
		IMapper _mapper) : INotificationSender
	{
		public NotificationType Type => NotificationType.SyetemNotification;
		private IGenericRepository<Domain.Entities.Notification, Guid> Repo => _unitOfWork.GetRepository<Domain.Entities.Notification, Guid>();
		public async Task SendAsync(NotificationDTO notification)
		{
			var notificationDB = _mapper.Map<Domain.Entities.Notification>(notification);
			await Repo.AddAsync(notificationDB);
			await _unitOfWork.SaveChangesAsync();
			await _hub
					.Clients
					.User(notification.ReceiverId.ToString())
					.SendAsync("ReceiveNotification", _mapper.Map<NotificationDTO>(notificationDB));
		}
	}
}
