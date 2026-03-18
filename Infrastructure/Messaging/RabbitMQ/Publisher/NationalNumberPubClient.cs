using Messaging.AsyncMessaging.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Services.Abstraction.Contracts;
using Shared;
using Shared.Enums.Logging;
using System.Text;

namespace Messaging.AsyncMessaging.Publisher
{
	public class NationalNumberPubClient : INationalNumberPubClient, IAsyncDisposable 
	{
		private readonly IRabbitMQConnection _connection;
		private readonly RabbitMQSettings _settings;
		private readonly IChannel _channel;
		private readonly ILogger<NationalNumberPubClient> _logger;
		private bool _disposed;

		public NationalNumberPubClient(IRabbitMQConnection rabbitMQConnection, IOptions<RabbitMQSettings> options, ILogger<NationalNumberPubClient> logger)
		{
			_logger = logger;
			_settings = options.Value;
			_connection = rabbitMQConnection;

			var publisherLog = new LogEntry
			{
				Category = Category.NationalNumberPublisher.ToString(),
				CategoryAction = CategoryAction.Initialize.ToString(),
			};

			try
			{
				_channel = _connection.GetConnection().CreateChannelAsync().GetAwaiter().GetResult();
				#region Log
				publisherLog.Timestamp = DateTime.Now;
				publisherLog.Level = "Information";
				publisherLog.RenderedMessage = $"Successfully connected to RabbitMQ at {DateTime.Now}";
				publisherLog.AdditionalData = $"The National Number Publisher successfully established a connection to RabbitMQ. This client will be used to publish the entered national number during user registration into the system";
				_logger.LogInformation("{@LogDetails}", publisherLog);
				#endregion
			}
			catch (Exception ex)
			{
				#region Log
				publisherLog.Timestamp = DateTime.Now;
				publisherLog.Level = "Fatal";
				publisherLog.RenderedMessage = $"Error connecting to RabbitMQ at {DateTime.Now}";
				publisherLog.AdditionalData = $"The National Number Publisher failed to establish a connection to RabbitMQ. This client will not be able to publish the entered national number during user registration into the system.";
				publisherLog.Exception = ex.ToString();
				publisherLog.ExceptionMessage = ex.Message;
				publisherLog.ExceptionDetail = ex.StackTrace;
				_logger.LogCritical("{@LogDetails}", publisherLog);
				#endregion
				throw new InvalidOperationException("Failed to establish RabbitMQ connection for National Number Publisher.", ex);
			}

		}

		public async Task PublishUserNationalNumberAsync(string nationalNumber)
		{
			var publishMessageLog = new LogEntry
			{
				Category = Category.NationalNumberPublisher.ToString(),
				CategoryAction = CategoryAction.PublishMessage.ToString(),
			};
			using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(_settings.TimeoutSeconds));
			try
			{
				if (_channel is null || !_channel.IsOpen || !_connection.GetConnection().IsOpen)
					throw new InvalidOperationException("RabbitMQ channel is closed or unavailable.");

				await _channel.ExchangeDeclareAsync(
					_settings.NationalNumberExchangeName, 
					ExchangeType.Fanout, 
					durable: true, 
					cancellationToken: cancellationTokenSource.Token
				);
				var body = Encoding.UTF8.GetBytes(nationalNumber);
				var properties = new BasicProperties
				{
					DeliveryMode = DeliveryModes.Persistent
				};
				await _channel.BasicPublishAsync(
					exchange: _settings.NationalNumberExchangeName,
					routingKey: "",
					mandatory: false,
					body: body,
					basicProperties: properties, // make message persistent
					cancellationToken: cancellationTokenSource.Token
				);

				#region Log
				publishMessageLog.Timestamp = DateTime.Now;
				publishMessageLog.Level = "Information";
				publishMessageLog.RenderedMessage = $"Published national number to RabbitMQ at {DateTime.Now}";
				publishMessageLog.AdditionalData = $"The National Number Publisher successfully published the national number {nationalNumber} to Message Broker.";
				_logger.LogInformation("{@LogDetails}", publishMessageLog);
				#endregion
			}
			catch(Exception ex)
			{
				#region Log
				publishMessageLog.Timestamp = DateTime.Now;
				publishMessageLog.Level = "Error";
				publishMessageLog.RenderedMessage = $"Failed to publish national number to RabbitMQ at {DateTime.Now}";
				publishMessageLog.AdditionalData = $"The National Number Publisher failed to publish the national number {nationalNumber} to Message Broker, External User Data might not be retrieved.";
				publishMessageLog.Exception = ex.ToString();
				publishMessageLog.ExceptionMessage = ex.Message;
				publishMessageLog.ExceptionDetail = ex.StackTrace;
				_logger.LogError("{@LogDetails}", publishMessageLog);
				#endregion
			}
		}

		public async ValueTask DisposeAsync()
		{
			var disposeLog = new LogEntry
			{
				Category = Category.NationalNumberPublisher.ToString(),
				CategoryAction = CategoryAction.Dispose.ToString(),
			};
			if (_disposed) return;
			try
			{
				if (_channel != null)
					await _channel.CloseAsync();

				_channel?.Dispose();
				#region Log
				disposeLog.Timestamp = DateTime.Now;
				disposeLog.Level = "Information";
				disposeLog.RenderedMessage = $"Successfully closed RabbitMQ channel at {DateTime.Now}";
				_logger.LogInformation("{@LogDetails}", disposeLog);
				#endregion
			}
			catch (Exception ex)
			{
				#region Log
				disposeLog.Timestamp = DateTime.Now;
				disposeLog.Level = "Error";
				disposeLog.RenderedMessage = $"Error closing RabbitMQ channel at {DateTime.Now}";
				disposeLog.Exception = ex.ToString();
				disposeLog.ExceptionMessage = ex.Message;
				disposeLog.ExceptionDetail = ex.StackTrace;
				_logger.LogError("{@LogDetails}", disposeLog);
				#endregion
			}
			_disposed = true;
			GC.SuppressFinalize(this);
		}

	}
}
