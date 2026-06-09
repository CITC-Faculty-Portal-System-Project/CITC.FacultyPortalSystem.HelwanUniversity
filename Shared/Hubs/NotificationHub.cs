using Microsoft.AspNetCore.SignalR;

namespace Shared.Hubs
{
	public class NotificationHub : Hub
	{
        public override Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;

            return base.OnConnectedAsync();
        }
    }
}
