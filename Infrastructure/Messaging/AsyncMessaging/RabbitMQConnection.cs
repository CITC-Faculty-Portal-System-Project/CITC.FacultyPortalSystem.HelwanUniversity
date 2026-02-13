using Messaging.AsyncMessaging.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Messaging.AsyncMessaging
{
	public class RabbitMQConnection : IRabbitMQConnection, IAsyncDisposable
	{
        private IConnection? _connection;
        private readonly RabbitMQConnectionSettings _settings;
		private bool _disposed;

		public RabbitMQConnection(IOptions<RabbitMQConnectionSettings> options)
        {
            _settings = options.Value;
			var factory = new ConnectionFactory
			{
				HostName = _settings.Host,
				Port = _settings.Port,
				UserName = _settings.Username,
				Password = _settings.Password,
				AutomaticRecoveryEnabled = true,
				NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
			};
			_connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
		}
        public IConnection GetConnection() => _connection ?? throw new InvalidOperationException("RabbitMQ connection is not established.");

		public async ValueTask DisposeAsync()
		{
			if (_disposed) return;

			try
			{
				if (_connection != null)
					await _connection.CloseAsync();

				_connection?.Dispose();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"--> Could not dispose RabbitMQ resources: {ex.Message}");
			}

			_disposed = true;
			GC.SuppressFinalize(this);
		}
	}
}
