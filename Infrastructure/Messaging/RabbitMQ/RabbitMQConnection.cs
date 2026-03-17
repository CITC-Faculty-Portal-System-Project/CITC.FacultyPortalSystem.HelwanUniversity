using Messaging.AsyncMessaging.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Shared;
using Shared.Enums.Logging;

namespace Messaging.AsyncMessaging
{
	public class RabbitMQConnection : IRabbitMQConnection, IAsyncDisposable
	{
        private IConnection? _connection;
        private readonly RabbitMQConnectionSettings _settings;
		private readonly ILogger<RabbitMQConnection> _logger;
		private bool _disposed;


		public RabbitMQConnection(IOptions<RabbitMQConnectionSettings> options, ILogger<RabbitMQConnection> logger)
        {
            _settings = options.Value;
			_logger = logger;

			var  connectionLog = new LogEntry
			{
				Category = Category.Connection.ToString(),
				CategoryAction = CategoryAction.Initialize.ToString()
			};

			try
			{
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
				#region Log
				connectionLog.Timestamp = DateTime.Now;
				connectionLog.RenderedMessage = $"RabbitMQ connection established successfully";
				connectionLog.Level = "Information";
				connectionLog.AdditionalData = $"Connection details => Host: {_settings.Host}, Port: {_settings.Port}";
				_logger.LogInformation("{@LogDetails}", connectionLog);
				#endregion
			}
			catch (Exception ex)
			{
				#region Log
				connectionLog.Timestamp = DateTime.Now;
				connectionLog.RenderedMessage = $"Failed to establish RabbitMQ connection";
				connectionLog.Level = "Fatal";
				connectionLog.AdditionalData = $"Connection details => Host: {_settings.Host}, Port: {_settings.Port}";
				connectionLog.Exception = ex.ToString();
				connectionLog.ExceptionMessage = ex.Message;
				connectionLog.ExceptionDetail = ex.StackTrace;
				_logger.LogCritical("{@LogDetails}", connectionLog);
				#endregion
			}
		}
        public IConnection GetConnection()
		{
			var connectionLog = new LogEntry
			{
				Category = Category.Connection.ToString(),
				CategoryAction = CategoryAction.GetConnection.ToString()
			};
			try
			{
				if (_connection is null)
					throw new InvalidOperationException("RabbitMQ connection is not initialized.");
				return _connection;
			}
			catch (Exception ex)
			{
				#region Log
				connectionLog.Exception = ex.ToString();
				connectionLog.ExceptionMessage = ex.Message;
				connectionLog.ExceptionDetail = ex.StackTrace;
				connectionLog.Level = "Fatal";
				connectionLog.Timestamp = DateTime.Now;
				connectionLog.RenderedMessage = "Failed to retrieve RabbitMQ connection.";
				_logger.LogCritical("{@LogDetails}", connectionLog);
				#endregion
				return null!;
			}
		}
		public async ValueTask DisposeAsync()
		{
			var disposeLog = new LogEntry
			{
				Category = Category.Connection.ToString(),
				CategoryAction = CategoryAction.Dispose.ToString(),
			};
			if(_disposed) return;
			try
			{
				if (_connection is { IsOpen: true })
					await _connection.CloseAsync();

				_connection?.Dispose();
				_connection = null;
				#region Log
				disposeLog.Timestamp = DateTime.Now;
				disposeLog.RenderedMessage = "RabbitMQ connection disposed successfully.";
				disposeLog.Level = "Information";
				_logger.LogInformation("{@LogDetails}", disposeLog);
				#endregion
			}
			catch (Exception ex)
			{
				#region Log
				disposeLog.Timestamp = DateTime.Now;
				disposeLog.RenderedMessage = "Failed to dispose RabbitMQ connection.";
				disposeLog.Level = "Error";
				disposeLog.Exception = ex.ToString();
				disposeLog.ExceptionMessage = ex.Message;
				disposeLog.ExceptionDetail = ex.StackTrace;
				disposeLog.AdditionalData = $"Failed to dispose Connection of details => Host: {_settings.Host}, Port: {_settings.Port}";
				_logger.LogError("{@LogDetails}", disposeLog);
				#endregion
			}
			_disposed = true;
			GC.SuppressFinalize(this);
		}
	}
}
