using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Shared.Dtos.ResearchesModule;
using Shared.Hubs;
using StackExchange.Redis;

public class RedisSubscriberService(IConnectionMultiplexer _redis 
        , IHubContext<ResearchFetchingProgressTrackingHub> _hubContext) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var sub = _redis.GetSubscriber();

        await sub.SubscribeAsync(new RedisChannel("research_progress:*", RedisChannel.PatternMode.Pattern),
            async (channel, message) =>
            {
                var progress = JsonConvert.DeserializeObject<ResearchFetchingProgressDTO>(message);

                var channelName = channel.ToString();
                var researcherId = channelName.Split(':')[1];

                await _hubContext.Clients
                    .Group($"research:{researcherId}")
                    .SendAsync("ReceiveProgress", progress);
            });

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}