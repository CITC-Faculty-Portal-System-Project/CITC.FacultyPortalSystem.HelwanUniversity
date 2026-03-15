using Messaging.AsyncMessaging.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services.Abstraction.Contracts;
using Shared;
using Shared.Enums.Logging;
using System.Text;

namespace Messaging.AsyncMessaging.Consumer
{
	public class ResearchDataConsumerClient : BackgroundService
	{
		private readonly IServiceScopeFactory _serviceScope;
		private readonly IRabbitMQConnection _connection;
		private readonly RabbitMQSettings _settings;
		private readonly ILogger<ResearchDataConsumerClient> _logger;
		private IChannel? _channel;
		private IChannel? _publisherChannel;
		public ResearchDataConsumerClient(IServiceScopeFactory serviceScope,
			IRabbitMQConnection connection,
			 IOptions<RabbitMQSettings> options,
			 ILogger<ResearchDataConsumerClient> logger)
		{
			_settings = options.Value;
			_serviceScope = serviceScope;
			_connection = connection;
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			var executeBackgroundLog = new LogEntry
			{
				Category = Category.ResearchDataConsumer.ToString(),
				CategoryAction = CategoryAction.BackgroundExecution.ToString(),
			};
			try
			{
				await InitializeRabbitMQAsync(cancellationToken);
				await StartConsumingAsync(cancellationToken);
				await Task.Delay(Timeout.Infinite, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				#region Log
				executeBackgroundLog.Timestamp = DateTime.Now;
				executeBackgroundLog.Level = "Warning";
				executeBackgroundLog.RenderedMessage = "Consumer cancellation requested. Shutting down";
				executeBackgroundLog.AdditionalData = "Cancellation of Background Consumer is requested -> Shutting down";
				_logger.LogWarning("{@LogDetails}", executeBackgroundLog);
				#endregion
			}
			catch (Exception ex)
			{
				#region Log
				executeBackgroundLog.Timestamp = DateTime.Now;
				executeBackgroundLog.Level = "Error";
				executeBackgroundLog.RenderedMessage = $"RabbitMQ consumer Exception {DateTime.Now}";
				executeBackgroundLog.AdditionalData = "An exception occurred in the background consuming process of the Research Data Consumer Client.";
				executeBackgroundLog.Exception = ex.ToString();
				executeBackgroundLog.ExceptionMessage = ex.Message;
				executeBackgroundLog.ExceptionDetail = ex.StackTrace;
				_logger.LogError("{@LogDetails}", executeBackgroundLog);
				#endregion
			}
		}

		public override async Task StopAsync(CancellationToken cancellationToken)
		{
			var stopBackgroundLog = new LogEntry
			{
				Category = Category.ResearchDataConsumer.ToString(),
				CategoryAction = CategoryAction.StopBackgroundExecution.ToString(),
			};

			if (_channel is not null)
			{
				try
				{
					if (_channel.IsOpen)
						await _channel.CloseAsync(cancellationToken: cancellationToken);
					#region Log
					stopBackgroundLog.Timestamp = DateTime.Now;
					stopBackgroundLog.Level = "Information";
					stopBackgroundLog.RenderedMessage = "Consumer channels closed successfully.";
					_logger.LogInformation("{@LogDetails}", stopBackgroundLog);
					#endregion
				}
				catch (Exception ex)
				{
					#region Log
					stopBackgroundLog.Timestamp = DateTime.Now;
					stopBackgroundLog.Level = "Error";
					stopBackgroundLog.RenderedMessage = $"Failed to close consumer channel at {DateTime.Now}";
					stopBackgroundLog.Exception = ex.ToString();
					stopBackgroundLog.ExceptionMessage = ex.Message;
					stopBackgroundLog.ExceptionDetail = ex.StackTrace;
					_logger.LogError("{@LogDetails}", stopBackgroundLog);
					#endregion
				}
			}
			if (_publisherChannel is not null)
			{
				try
				{
					if (_publisherChannel.IsOpen)
						await _publisherChannel.CloseAsync(cancellationToken: cancellationToken);
					#region Log
					stopBackgroundLog.Timestamp = DateTime.Now;
					stopBackgroundLog.Level = "Information";
					stopBackgroundLog.RenderedMessage = "DLQ Publisher channel closed successfully.";
					_logger.LogInformation("{@LogDetails}", stopBackgroundLog);
					#endregion
				}
				catch
				(Exception ex)
				{
					#region Log
					stopBackgroundLog.Timestamp = DateTime.Now;
					stopBackgroundLog.Level = "Error";
					stopBackgroundLog.RenderedMessage = $"Failed to close DLQ Publisher channel at {DateTime.Now}";
					stopBackgroundLog.Exception = ex.ToString();
					stopBackgroundLog.ExceptionMessage = ex.Message;
					stopBackgroundLog.ExceptionDetail = ex.StackTrace;
					_logger.LogError("{@LogDetails}", stopBackgroundLog);
					#endregion
				}
			}
		}

		public override void Dispose()
		{
			var disposeLog = new LogEntry
			{
				Category = Category.ResearchDataConsumer.ToString(),
				CategoryAction = CategoryAction.Dispose.ToString(),
				AdditionalData = "The Background Disposing Method in the Research Data Consumer Client [Disposing Channels]"
			};
			try
			{
				_channel?.Dispose();
				_publisherChannel?.Dispose();
				#region Log
				disposeLog.Timestamp = DateTime.Now;
				disposeLog.Level = "Information";
				disposeLog.RenderedMessage = "Consumer/DLQ channels disposed successfully.";
				_logger.LogInformation("{@LogDetails}", disposeLog);
				#endregion
			}
			catch (Exception ex)
			{
				#region Log
				disposeLog.Timestamp = DateTime.Now;
				disposeLog.Level = "Error";
				disposeLog.RenderedMessage = $"Failed to dispose channels at {DateTime.Now}";
				disposeLog.Exception = ex.ToString();
				disposeLog.ExceptionMessage = ex.Message;
				disposeLog.ExceptionDetail = ex.StackTrace;
				_logger.LogError("{@LogDetails}", disposeLog);
				#endregion
			}
			base.Dispose();
		}

		#region Initialization Methods

		private async Task InitializeRabbitMQAsync(CancellationToken cancellationToken)
		{
			var initalLog = new LogEntry
			{
				Category = Category.ResearchDataConsumer.ToString(),
				CategoryAction = CategoryAction.Initialize.ToString(),
			};
			try
			{
				_channel = await _connection.GetConnection().CreateChannelAsync(cancellationToken: cancellationToken);
				_publisherChannel = await _connection.GetConnection().CreateChannelAsync(cancellationToken: cancellationToken);
				#region Log
				initalLog.Timestamp = DateTime.Now;
				initalLog.Level = "Information";
				initalLog.RenderedMessage = "RabbitMQ channels created successfully.";
				initalLog.AdditionalData = "Channels for research data consuming and DLQ publishing are initialized";
				_logger.LogInformation("{@LogDetails}", initalLog);
				#endregion
			}
			catch (Exception ex)
			{
				#region Log
				initalLog.Exception = ex.ToString();
				initalLog.ExceptionMessage = ex.Message;
				initalLog.ExceptionDetail = ex.StackTrace;
				initalLog.Timestamp = DateTime.Now;
				initalLog.Level = "Fatal";
				initalLog.RenderedMessage = $"Failed to initialize RabbitMQ channels";
				initalLog.AdditionalData = "Exception occurred while creating channels for consuming research data and DLQ publishing";
				_logger.LogCritical("{@LogDetails}", initalLog);
				#endregion
			}
			await DeclareExchangeAndQueueAsync(cancellationToken);
		}
		private async Task DeclareExchangeAndQueueAsync(CancellationToken cancellationToken)
		{
			var declareBackgroundLog = new LogEntry
			{
				Category = Category.ResearchDataConsumer.ToString(),
				CategoryAction = CategoryAction.DeclareQueueAndExchange.ToString(),
			};
			try
			{
				if (_channel is null)
					throw new InvalidOperationException("Channel not initialized.");

				await _channel.ExchangeDeclareAsync(
					exchange: _settings.ResearchDataExchangeName,
					type: ExchangeType.Direct,
					durable: true,
					autoDelete: false,
					cancellationToken: cancellationToken);

				#region DL

				await _channel.ExchangeDeclareAsync(
					exchange: _settings.ResearchDataDLX,
					type: ExchangeType.Direct,
					durable: true,
					autoDelete: false,
					cancellationToken: cancellationToken);

				await _channel.QueueDeclareAsync(
						queue: _settings.ResearchDataDLQ,
						durable: true,
						exclusive: false,
						autoDelete: false,
						arguments: null,
						cancellationToken: cancellationToken);

				await _channel.QueueBindAsync(
					queue: _settings.ResearchDataDLQ,
					exchange: _settings.ResearchDataDLX,
					routingKey: _settings.ResearchDataDLRK,
					cancellationToken: cancellationToken);

				var args = new Dictionary<string, object>
			{
				{ "x-dead-letter-exchange", _settings.ResearchDataDLX},
				{ "x-dead-letter-routing-key", _settings.ResearchDataDLRK}
			};

				#endregion

				await _channel.QueueDeclareAsync(
						queue: _settings.ResearchDataQueueName,
						durable: true,
						exclusive: false,
						autoDelete: false,
						arguments: args!,
						cancellationToken: cancellationToken);

				await _channel.QueueBindAsync(
					queue: _settings.ResearchDataQueueName,
					exchange: _settings.ResearchDataExchangeName,
					routingKey: _settings.ResearchDataRoutingKey,
					cancellationToken: cancellationToken);

				#region Log
				declareBackgroundLog.Timestamp = DateTime.Now;
				declareBackgroundLog.Level = "Information";
				declareBackgroundLog.RenderedMessage = "RabbitMQ exchange and queue declared successfully.";
				declareBackgroundLog.AdditionalData = $"Declared Exchange: [{_settings.ResearchDataExchangeName}] of type [Direct], Queue: [{_settings.ResearchDataQueueName}], DLX: [{_settings.ResearchDataDLX}], DLQ: [{_settings.ResearchDataDLQ}]";
				_logger.LogInformation("{@LogDetails}", declareBackgroundLog);
				#endregion
			}
			catch (Exception ex)
			{
				#region Log
				declareBackgroundLog.Timestamp = DateTime.Now;
				declareBackgroundLog.Level = "Fatal";
				declareBackgroundLog.RenderedMessage = $"Failed to declare RabbitMQ exchange and queue";
				declareBackgroundLog.Exception = ex.ToString();
				declareBackgroundLog.ExceptionMessage = ex.Message;
				declareBackgroundLog.ExceptionDetail = ex.StackTrace;
				declareBackgroundLog.AdditionalData = $"Exception occurred while declaring exchange: [{_settings.ResearchDataExchangeName}] and queue: [{_settings.ResearchDataQueueName}] for research data consuming and Deadletter queue: [{_settings.ResearchDataDLQ}] and exchange: [{_settings.ResearchDataDLX}]";
				_logger.LogCritical("{@LogDetails}", declareBackgroundLog);
				#endregion
			}
		}

		#endregion

		#region Consumers

		private async Task StartConsumingAsync(CancellationToken cancellationToken)
		{
			var consumeBackgroundLog = new LogEntry
			{
				Category = Category.ResearchDataConsumer.ToString(),
				CategoryAction = CategoryAction.ConsumeMessages.ToString(),
			};

			if (cancellationToken.IsCancellationRequested)
			{
				#region Log
				consumeBackgroundLog.Timestamp = DateTime.Now;
				consumeBackgroundLog.Level = "Warning";
				consumeBackgroundLog.RenderedMessage = "Cancellation requested before the start of consuming";
				consumeBackgroundLog.AdditionalData = "Cancellation was requested before starting the consuming process. No messages will be consumed.";
				_logger.LogWarning("{@LogDetails}", consumeBackgroundLog);
				#endregion
				return;
			}
			try
			{
				if (_channel is null || !_channel.IsOpen)
					throw new InvalidOperationException("Channel not available for consuming");

				await _channel!.BasicQosAsync(0, prefetchCount: 30, global: false);

				var consumer = new AsyncEventingBasicConsumer(_channel!);
				consumer.ReceivedAsync += HandleMessageAsync;

				await _channel!.BasicConsumeAsync(
					queue: _settings.ResearchDataQueueName,
					autoAck: false,
					consumer: consumer,
					cancellationToken: cancellationToken);

				#region Log
				consumeBackgroundLog.Timestamp = DateTime.Now;
				consumeBackgroundLog.Level = "Information";
				consumeBackgroundLog.RenderedMessage = "Started consuming messages successfully";
				consumeBackgroundLog.AdditionalData = $"Consumer is consuming from queue: [{_settings.ResearchDataQueueName}] with prefetch count: [{30}]";
				_logger.LogInformation("{@LogDetails}", consumeBackgroundLog);
				#endregion
			}
			catch (Exception ex)
			{
				#region Log
				consumeBackgroundLog.Timestamp = DateTime.Now;
				consumeBackgroundLog.Level = "Fatal";
				consumeBackgroundLog.RenderedMessage = $"Failed to start consuming messages";
				consumeBackgroundLog.Exception = ex.ToString();
				consumeBackgroundLog.ExceptionMessage = ex.Message;
				consumeBackgroundLog.ExceptionDetail = ex.StackTrace;
				consumeBackgroundLog.AdditionalData = $"Exception occurred while starting consumer for queue: [{_settings.ResearchDataQueueName}]";
				_logger.LogCritical("{@LogDetails}", consumeBackgroundLog);
				#endregion
			}
		}

		private async Task HandleMessageAsync(object sender, BasicDeliverEventArgs ea)
		{
			var handleMessageLog = new LogEntry
			{
				Category = Category.ResearchDataConsumer.ToString(),
				CategoryAction = CategoryAction.MessageHandling.ToString(),
			};
			try
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);

				using var scope = _serviceScope.CreateScope();
				var service = scope.ServiceProvider.GetRequiredService<IExternalDataHandlingService>();
				await service.ResearchDataHandle(message);
				await _channel!.BasicAckAsync(ea.DeliveryTag, false);
				#region Log
				handleMessageLog.Timestamp = DateTime.Now;
				handleMessageLog.Level = "Information";
				handleMessageLog.RenderedMessage = "Message processed successfully";
				handleMessageLog.AdditionalData = $"Processed Research Data from queue: [{_settings.ResearchDataQueueName}]";
				_logger.LogInformation("{@LogDetails}", handleMessageLog);
				#endregion
			}
			catch (FormatException ex)
			{
				await _channel!.BasicRejectAsync(ea.DeliveryTag, requeue: false);
				#region Log
				handleMessageLog.Exception = ex.ToString();
				handleMessageLog.ExceptionMessage = ex.Message;
				handleMessageLog.ExceptionDetail = ex.StackTrace;
				handleMessageLog.Timestamp = DateTime.Now;
				handleMessageLog.Level = "Error";
				handleMessageLog.RenderedMessage = $"Message rejected, Invalid message format";
				handleMessageLog.AdditionalData = $"Message with invalid format was rejected from queue: [{_settings.ResearchDataQueueName}] and sent to DLQ: [{_settings.ResearchDataDLQ}] without retrying. Message content: {Encoding.UTF8.GetString(ea.Body.ToArray())}";
				_logger.LogError("{@LogDetails}", handleMessageLog);
				#endregion
			}
			catch (Exception ex)
			{
				var retryCount = GetRetryCount(ea.BasicProperties.Headers);
				if (retryCount < _settings.MaxRetryCount)
				{
					await RepublishWithRetryAsync(ea, retryCount + 1);
					await _channel!.BasicAckAsync(ea.DeliveryTag, false);
					#region Log
					handleMessageLog.Timestamp = DateTime.Now;
					handleMessageLog.Level = "Warning";
					handleMessageLog.RenderedMessage = $"Message processing failed, retrying";
					handleMessageLog.Exception = ex.ToString();
					handleMessageLog.ExceptionMessage = ex.Message;
					handleMessageLog.ExceptionDetail = ex.StackTrace;
					handleMessageLog.AdditionalData = $"Processing of message failed. The message will be Re-Queued to Retry. Retry count: [{retryCount + 1} out of {_settings.MaxRetryCount}]. Message content: {Encoding.UTF8.GetString(ea.Body.ToArray())}";
					_logger.LogWarning("{@LogDetails}", handleMessageLog);
					#endregion
				}
				else
				{
					await _channel!.BasicRejectAsync(ea.DeliveryTag, requeue: false);
					#region Log
					handleMessageLog.Timestamp = DateTime.Now;
					handleMessageLog.Level = "Error";
					handleMessageLog.RenderedMessage = $"Message processing failed, max retries exceeded";
					handleMessageLog.Exception = ex.ToString();
					handleMessageLog.ExceptionMessage = ex.Message;
					handleMessageLog.ExceptionDetail = ex.StackTrace;
					handleMessageLog.AdditionalData = $"Processing of message failed. Max retry count of [{_settings.MaxRetryCount}] exceeded. The message will be rejected and sent to DLQ: [{_settings.ResearchDataDLQ}]. Message content: {Encoding.UTF8.GetString(ea.Body.ToArray())}";
					_logger.LogError("{@LogDetails}", handleMessageLog);
					#endregion
				}
			}
		}

		private int GetRetryCount(IDictionary<string, object?>? headers)
		{
			if (headers == null || !headers.TryGetValue("data-retry-count", out var value))
				return 0;

			return value switch
			{
				byte[] bytes => int.Parse(Encoding.UTF8.GetString(bytes)),
				int i => i,
				_ => 0
			};
		}

		private async Task RepublishWithRetryAsync(BasicDeliverEventArgs ea, int retryCount)
		{
			var republishLog = new LogEntry
			{
				Category = Category.ResearchDataConsumer.ToString(),
				CategoryAction = CategoryAction.PublishMessage.ToString(),
			};
			if (_publisherChannel == null || !_publisherChannel.IsOpen)
			{
				try
				{
					_publisherChannel = await _connection.GetConnection().CreateChannelAsync(cancellationToken: default);
				}
				catch (Exception)
				{
					#region Log
					republishLog.Timestamp = DateTime.Now;
					republishLog.Level = "Fatal";
					republishLog.RenderedMessage = $"Failed to recreate publisher channel for retrying message";
					republishLog.AdditionalData = $"An error occurred while trying to recreate the publisher channel for retrying the message. The message containing the research data will not be retried and will be rejected without requeuing. Message content: {Encoding.UTF8.GetString(ea.Body.ToArray())}";
					_logger.LogCritical("{@LogDetails}", republishLog);
					#endregion
					return;
				}
			}
			try
			{
				var props = new BasicProperties
				{
					Persistent = true,
					Headers = new Dictionary<string, object?>
				{
					{ "data-retry-count", retryCount }
				}
				};

				await _publisherChannel!.BasicPublishAsync(
					exchange: _settings.ResearchDataExchangeName,
					routingKey: _settings.ResearchDataRoutingKey,
					mandatory: false,
					basicProperties: props,
					body: ea.Body);

				#region Log
				republishLog.Timestamp = DateTime.Now;
				republishLog.Level = "Information";
				republishLog.RenderedMessage = $"Message republished for retry";
				republishLog.AdditionalData = $"Message has been republished to exchange: [{_settings.ResearchDataDLX}] for retrying. Current retry count: [{retryCount}] out of [{_settings.MaxRetryCount}]. Message content: {Encoding.UTF8.GetString(ea.Body.ToArray())}";
				_logger.LogInformation("{@LogDetails}", republishLog);
				#endregion
			}
			catch (Exception ex)
			{
				#region Log
				republishLog.Timestamp = DateTime.Now;
				republishLog.Level = "Fatal";
				republishLog.Exception = ex.ToString();
				republishLog.ExceptionMessage = ex.Message;
				republishLog.ExceptionDetail = ex.StackTrace;
				republishLog.RenderedMessage = $"Failed to re-publish messages for retrying";
				republishLog.AdditionalData = $"An error occurred while trying to re-publish for retrying the message. Message content: {Encoding.UTF8.GetString(ea.Body.ToArray())}";
				_logger.LogCritical("{@LogDetails}", republishLog);
				#endregion
			}
		}
		#endregion
	}
}
