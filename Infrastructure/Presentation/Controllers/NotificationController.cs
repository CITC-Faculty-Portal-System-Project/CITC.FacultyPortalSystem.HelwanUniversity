using Microsoft.AspNetCore.Authorization;
using Shared.Dtos.MessagingAndChattingModule;
using Shared.Dtos.Notification;

namespace Presentation.Controllers
{
	[Authorize]
	public class NotificationController(IServiceManager _serviceManager) : ApiController
	{
		[ProducesResponseType(typeof(NotificationDTO), StatusCodes.Status200OK)]
		[HttpPut("RemoveNotification/{notificationId:guid}")]
		public async Task<ActionResult<bool>> RemoveNotification(Guid notificationId)
			=> Ok(await _serviceManager.NotificationService.RemoveNotificationAsync(notificationId));

		[ProducesResponseType(typeof(NotificationDTO), StatusCodes.Status200OK)]
		[HttpPut("ViewNotification/{notificationId:Guid}")]
		public async Task<ActionResult> ViewNotification(Guid notificationId)
		{
			await _serviceManager.NotificationService.MarkAsViewedAsync(notificationId);
			return Ok();
		}
	}
}
