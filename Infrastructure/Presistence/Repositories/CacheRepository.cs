using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;

namespace Presistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer _connectionMultiplexer, JsonSerializerOptions _jsonOptions, ILogger<CacheRepository> _logger) : ICacheRepository
    {
        private readonly IDatabase _database = _connectionMultiplexer.GetDatabase();

        public async Task<string?> GetAsync(string key)
        {
            try
            {
                var value = await _database.StringGetAsync(key);

                return value.IsNullOrEmpty
                    ? default
                    : value.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Redis GET failed for {Key}", key);
                return default;
            }
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            try
            {
                string data;

                if (value is string s)
                {
                    data = s;
                }
                else
                {
                    data = JsonSerializer.Serialize(value, _jsonOptions);
                }

                await _database.StringSetAsync(key, data, duration);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Redis SET failed for key {Key}", key);
            }
        }
    }
}
