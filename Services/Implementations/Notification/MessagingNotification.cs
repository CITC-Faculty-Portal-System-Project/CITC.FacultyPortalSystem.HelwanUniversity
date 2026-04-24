using Microsoft.AspNetCore.SignalR;
using Services.Abstraction.Contracts.Notification;
using Services.Specifications.NotificationModule;
using Shared.Dtos.Notification;
using Shared.Enums.NotificationModule;
using Shared.Hubs;

namespace Services.Implementations.Notification
{
    public class MessagingNotification(IHubContext<NotificationHub> _hub,
		IUnitOfWork _unitOfWork,
		IMapper _mapper) : INotificationSender
	{
		public NotificationType Type => NotificationType.ChatMessage;
		private IGenericRepository<Domain.Entities.Notification, Guid> Repo => _unitOfWork.GetRepository<Domain.Entities.Notification, Guid>();
		public async Task SendAsync(NotificationDTO notification)
		{
			var notificationDB = await Repo.GetAsync(new NotificationSpecifications(notification.Source, notification.ReceiverId));

			if(notificationDB is null)
			{
				notificationDB = _mapper.Map<Domain.Entities.Notification>(notification);
				await Repo.AddAsync(notificationDB);
			}
			else
			{
				int unreadMessagesCount = await CountUnReadMessages(notification.Source, notification.ReceiverId);
				_mapper.Map(notification, notificationDB);
				notificationDB = UpdateNotification(notificationDB, unreadMessagesCount);
				Repo.Update(notificationDB);
			}
			await _unitOfWork.SaveChangesAsync();
			await _hub
					.Clients
					.User(notification.ReceiverId.ToString())
					.SendAsync("ReceiveNotification", _mapper.Map<NotificationDTO>(notificationDB));
		}

		#region HELPER
		//This method takes the Conversation Id and the Notification Receiver Id -> Get the Count of UnRead Messages for the [Receiver] in [Conversation]
		private async Task<int> CountUnReadMessages(string conversationId, Guid receiverId)
		{
			//---> Implement the logic to count un-read messages for the receiver in the conversation
			return 0; // Return the count of un-read messages
		}
		private Domain.Entities.Notification UpdateNotification(Domain.Entities.Notification notification, int countUnreadMessages)
		{
			notification.Message = countUnreadMessages == 1 ? "1 NEW UNREAD MESSAGE" : $"{countUnreadMessages} NEW UNREAD MESSAGES";
			notification.UpdatedAt = DateTime.Now;
			if (notification.IsDeleted || notification.IsRemoved)
			{
				notification.IsViewed = false;
				notification.IsRemoved = false;
				notification.IsDeleted = false;
			}
			return notification;
		}
		#endregion
	}
}
