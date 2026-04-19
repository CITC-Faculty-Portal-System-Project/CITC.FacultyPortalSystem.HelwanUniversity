
using Microsoft.AspNetCore.SignalR;
using Services.Global;
using Services.Specifications.AcademicDataModule.ContributionsModule;
using Services.Specifications.NotificationModule;
using Shared.Dtos.AcademicDataModule.ContributionsModule;
using Shared.Enums.Logging;
using Shared.Hubs;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

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

        public async Task MarkAsViewedAsync(Guid notificationId)
		{
			var notification = await Repo.GetAsync(new NotificationSpecifications(notificationId)) 
				?? throw NotFound();

			notification.IsViewed = true;
			notification.IsRemoved = true;
			notification.IsDeleted = true;

			Repo.Update(notification);
			await unitOfWork.SaveChangesAsync();
		}

		public async Task RemoveNotificationAsync(Guid notificationId)
		{
			var notification = await Repo.GetAsync(new NotificationSpecifications(notificationId))
				?? throw NotFound();

			notification.IsRemoved = true;

			Repo.Update(notification);
			await unitOfWork.SaveChangesAsync();
		}

		public async Task SendNotificationAsync(NotificationDTO notification)
		{
			var notificationDb = await Repo.GetAsync(new NotificationSpecifications(notification.Source, notification.ReceiverId));

			if(notificationDb is not null) //If exists { even if IsDeleted is true}
			{
				//----> Calculate the number of un-read messages between the sender and receiver using CountUnReadMessages() method
				//----> Update the notification with the new count using UpdateNotification() method
			}
			else //If not exists [null]
			{
				//Modify Dto body here
				var newNotification = mapper.Map<Domain.Entities.Notification>(notification);
				await Repo.AddAsync(newNotification);
			}
			await unitOfWork.SaveChangesAsync();

			await _hubContext
			.Clients
			.User(notification.ReceiverId.ToString())
			.SendAsync("ReceiveNotification", notification);
		}

		#region Helper Methods
		private int CountUnReadMessages()
		{
			//Check the number of messages unread by the receiver from the sender and return it
			return 0;
		}
		private NotificationDTO UpdateNotification()
		{
			//Update the Message body of the Notification with the new count of un-read messages and return the updated notification
			return new NotificationDTO();
		}
		#endregion
	}
}
