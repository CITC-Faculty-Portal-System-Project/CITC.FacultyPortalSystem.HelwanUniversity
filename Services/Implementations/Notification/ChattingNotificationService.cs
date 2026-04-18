
using Microsoft.AspNetCore.SignalR;
using Services.Global;
using Shared.Hubs;

namespace Services.Implementations.Notification
{
	public class ChattingNotificationService(IHubContext<NotificationHub> _hubContext,
		IUnitOfWork unitOfWork,
		IAuthenticationService authenticationService,
		IMapper mapper) : 
		BaseService<Domain.Entities.Notification,Guid>(unitOfWork, authenticationService, mapper),
		INotificationService
	{
		protected override string EntityName => "Notification";

		public Task MarkAsViewedAsync(Guid notificationId)
		{
			//1.Using Specification Design Pattern get a notification with notificationId
			//--> If exists
			//----> Update the notification with IsViewed = true and  IsRemoved = true and IsDeleted = true then save it to database.
			throw new NotImplementedException();
		}

		public Task RemoveNotificationAsync(Guid notificationId)
		{
			//1.Using Specification Design Pattern get a notification with notificationId
			//--> If exists
			//----> Update the notification with IsRemoved = true and save it to database
			throw new NotImplementedException();
		}

		public Task SendNotificationAsync(NotificationDto notification)
		{
			//1.Using Specification Design Pattern get a notification with notification.ReceiverId and notification.Sender
			//--> If exists {even if IsDeleted is true}
			//----> Calculate the number of un-read messages between the sender and receiver using CountUnReadMessages() method
			//----> Update the notification with the new count using UpdateNotification() method
			//--> If not exists [null]
			//----> Create a new notification and save it to database
			//----> Send the notification to the receiver using SignalR
			throw new NotImplementedException();
		}

		#region Helper Methods
		private int CountUnReadMessages()
		{
			//Check the number of messages unread by the receiver from the sender and return it
			return 0;
		}
		private NotificationDto UpdateNotification()
		{
			//Update the Message body of the Notification with the new count of un-read messages and return the updated notification
			return new NotificationDto();
		}
		#endregion
	}
}
