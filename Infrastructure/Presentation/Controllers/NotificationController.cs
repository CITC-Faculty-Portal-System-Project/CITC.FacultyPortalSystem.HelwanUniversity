using Microsoft.AspNetCore.Authorization;
using Shared;
using Shared.Dtos.MessagingAndChattingModule;
using Shared.Dtos.Notification;
using Shared.SpecificationParameters.NotificationsModule;

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

        [ProducesResponseType(typeof(CursorPaginatedResult<NotificationDTO  , Guid>), StatusCodes.Status200OK)]
        [HttpGet("UserNotifications")]
        public async Task<ActionResult<CursorPaginatedResult<NotificationDTO , Guid>>> GetAllUserNotifications([FromQuery]NotificationSpecificationsParameters parameters)
        {
            return Ok(await _serviceManager.NotificationService.GetUserNotificationsAsync(parameters));
        }
    }
}
