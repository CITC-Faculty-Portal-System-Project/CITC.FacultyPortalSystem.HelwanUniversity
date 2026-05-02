using Microsoft.AspNetCore.SignalR;

namespace Shared.Hubs
{
    public class ResearchFetchingProgressTrackingHub : Hub
    {
        public async Task JoinGroup(string researcherNationalNumber)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"research:{researcherNationalNumber}");
        }

        public async Task LeaveGroup(string researcherNationalNumber)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"research:{researcherNationalNumber}");
        }
    }
}
