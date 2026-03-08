using Messaging.AsyncMessaging.Consumer.Helpers;
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
using System.Collections.Concurrent;
using System.Text;

namespace Messaging.AsyncMessaging.Consumer
{
	public class ExternalDataConsumerClient : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IRabbitMQConnection _connection;
		private readonly RabbitMQSettings _settings;
		private readonly ILogger<ExternalDataConsumerClient> _logger;
		private readonly ConcurrentBag<IChannel> _channels = new();

        public ExternalDataConsumerClient(
            IRabbitMQConnection rabbitMQConnection, 
            IServiceScopeFactory scopeFactory,
			IOptions<RabbitMQSettings> options,
			ILogger<ExternalDataConsumerClient> logger)
        {
			_connection = rabbitMQConnection;
			_scopeFactory = scopeFactory;
			_settings = options.Value;
			_logger = logger;

		}

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
			var startLog = new LogEntry
			{
				Category = Category.ExternalDataConsumer.ToString(),
				CategoryAction = CategoryAction.Initialize.ToString(),
			};
			try
            {
                await InitializeRabbitMQAsync(cancellationToken);
                await base.StartAsync(cancellationToken);
            }
            catch (Exception ex)
            {
				#region Log
				startLog.Timestamp = DateTime.Now;
				startLog.Level = "Fatal";
				startLog.RenderedMessage = "Failed to initalize External Data Consumer";
				startLog.Exception = ex.ToString();
				startLog.ExceptionMessage = ex.Message;
				startLog.ExceptionDetail = ex.StackTrace;
				startLog.AdditionalData = $"An error occurred during the initialization of External Data Consumer. The consumer will not start consuming messages.";
				_logger.LogCritical("{@LogDetails}", startLog);
				#endregion
			}
        }

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var executeLog = new LogEntry
			{
				Category = Category.ExternalDataConsumer.ToString(),
				CategoryAction = CategoryAction.BackgroundExecution.ToString(),
			};

			try
			{ 
					await StartConsumersAsync(stoppingToken);
					await Task.Delay(Timeout.Infinite, stoppingToken);
			}
			catch (OperationCanceledException)
			{
				#region Log
				executeLog.Timestamp = DateTime.Now;
				executeLog.Level = "Warning";
				executeLog.RenderedMessage = "Consumer cancellation requested. Shutting down";
				executeLog.AdditionalData = "Cancellation of Background Consumer is requested -> Shutting down";
				_logger.LogWarning("{@LogDetails}", executeLog);
				#endregion
			}
			catch (Exception ex)
			{
				#region Log
				executeLog.Timestamp = DateTime.Now;
				executeLog.Level = "Error";
				executeLog.RenderedMessage = "RabbitMQ consumer Exception {DateTime.Now}";
				executeLog.Exception = ex.ToString();
				executeLog.ExceptionMessage = ex.Message;
				executeLog.ExceptionDetail = ex.StackTrace;
				executeLog.AdditionalData = $"An error occurred during the execution of the External Data Consumer.";
				_logger.LogError(ex, "{@LogDetails}", executeLog);
				#endregion
			}
		}

		public override async Task StopAsync(CancellationToken cancellationToken)
		{
			var stopExecutionLog = new LogEntry
			{
				Category = Category.ExternalDataConsumer.ToString(),
				CategoryAction = CategoryAction.StopBackgroundExecution.ToString(),
			};
			if (_channels.Any())
			{
				try
				{
					foreach (var channel in _channels)
					{ 
						if (channel.IsOpen) 
							await channel.CloseAsync(cancellationToken: cancellationToken); 
					}
					#region Log
					stopExecutionLog.Timestamp = DateTime.Now;
					stopExecutionLog.Level = "Information";
					stopExecutionLog.RenderedMessage = "Consumer channels closed successfully";
					stopExecutionLog.AdditionalData = "All consumer channels have been closed successfully.";
					_logger.LogInformation("{@LogDetails}", stopExecutionLog);
					#endregion
				}
				catch (Exception ex)
				{
					#region Log
					stopExecutionLog.Timestamp = DateTime.Now;
					stopExecutionLog.Level = "Error";
					stopExecutionLog.RenderedMessage = $"Failed to close consumer channels at {DateTime.Now}";
					stopExecutionLog.Exception = ex.ToString();
					stopExecutionLog.ExceptionMessage = ex.Message;
					stopExecutionLog.ExceptionDetail = ex.StackTrace;
					_logger.LogError("{@LogDetails}", stopExecutionLog);
					#endregion
				}
			}
		}

		public override void Dispose()
		{
			var disposeLog = new LogEntry
			{
				Category = Category.ExternalDataConsumer.ToString(),
				CategoryAction = CategoryAction.Dispose.ToString(),
			};

			if (_channels.Any())
			{
				foreach (var channel in _channels)
				{
					try
					{
						channel.Dispose();
					}
					catch (Exception ex)
					{
						#region Log
						disposeLog.Timestamp = DateTime.Now;
						disposeLog.Level = "Error";
						disposeLog.RenderedMessage = $"Failed to dispose consumer channel at {DateTime.Now}";
						disposeLog.Exception = ex.ToString();
						disposeLog.ExceptionMessage = ex.Message;
						disposeLog.ExceptionDetail = ex.StackTrace;
						disposeLog.AdditionalData = "An error occurred while trying to dispose a consumer channel.";
						_logger.LogError("{@LogDetails}", disposeLog);
						#endregion
					}
				}
				#region Log
				disposeLog.Timestamp = DateTime.Now;
				disposeLog.Level = "Information";
				disposeLog.RenderedMessage = "Consumer channels disposed successfully";
				disposeLog.AdditionalData = "All consumer channels have been disposed successfully.";
				_logger.LogInformation("{@LogDetails}", disposeLog);
				#endregion
			}
			base.Dispose();
		}


		#region Helpers
		private async Task InitializeRabbitMQAsync(CancellationToken cancellationToken)
        {
			var initializerLog = new LogEntry
			{
				Category = Category.ExternalDataConsumer.ToString(),
				CategoryAction = CategoryAction.DeclareQueueAndExchange.ToString(),
			};
			try
			{
				using var setupChannel = await _connection.GetConnection().CreateChannelAsync(cancellationToken: cancellationToken);

				await setupChannel.ExchangeDeclareAsync(
					RabbitMQConstants.ExchangeName,
					ExchangeType.Direct,
					durable: true,
					cancellationToken: cancellationToken);

				#region DL Exchange/Prop

				await setupChannel.ExchangeDeclareAsync(
					_settings.ExternalDataDLX,
					ExchangeType.Direct,
					durable: true,
					autoDelete: false,
					cancellationToken: cancellationToken);

				var args = new Dictionary<string, object>
			{
				{ "x-dead-letter-exchange", _settings.ExternalDataDLX},
				{ "x-dead-letter-routing-key", _settings.ExternalDataDLRK}
			};

				#endregion

				var queues = QueueInitializer.InitializeQueues();

				foreach (var queue in queues)
				{
					await setupChannel.QueueDeclareAsync(
						queue.QueueName,
						durable: true,
						exclusive: false,
						autoDelete: false,
						arguments: args!,
						cancellationToken: cancellationToken);

					await setupChannel.QueueBindAsync(
						queue.QueueName,
						RabbitMQConstants.ExchangeName,
						queue.RoutingKey,
						cancellationToken: cancellationToken);
				}

				#region DLQ/Bind

				await setupChannel.QueueDeclareAsync(
					_settings.ExternalDataDLQ,
					durable: true,
					exclusive: false,
					autoDelete: false,
					arguments: null,
					cancellationToken: cancellationToken);

				await setupChannel.QueueBindAsync(
					_settings.ExternalDataDLQ,
					_settings.ExternalDataDLX,
					_settings.ExternalDataDLRK,
					cancellationToken: cancellationToken);

				#endregion

				#region Log
				initializerLog.Timestamp = DateTime.Now;
				initializerLog.Level = "Information";
				initializerLog.RenderedMessage = "External Data Consumer Setup Completed Successfully";
				initializerLog.AdditionalData = $"Declare the Consumer Exchanges and Queues + Dead Letter Queue {_settings.ExternalDataDLQ} with routing key {_settings.ExternalDataDLRK}";
				_logger.LogInformation("{@LogEntry}", initializerLog);
				#endregion
			}
			catch (Exception ex)
			{
				#region Log
				initializerLog.Timestamp = DateTime.Now;
				initializerLog.Level = "Error";
				initializerLog.RenderedMessage = "External Data Consumer Setup Failed";
				initializerLog.AdditionalData = $"Error during External Data Setup [Exchanges - Queues]";
				initializerLog.Exception = ex.ToString();
				initializerLog.ExceptionMessage = ex.Message;
				initializerLog.ExceptionDetail = ex.StackTrace;
				_logger.LogError(ex, "{@LogEntry}", initializerLog);
				#endregion
			}
		}
        private async Task StartConsumersAsync(CancellationToken cancellationToken)
        {
			var consumerLog = new LogEntry
			{
				Category = Category.ExternalDataConsumer.ToString(),
				CategoryAction = CategoryAction.ConsumeMessages.ToString(),
			};
			try
			{
				var queues = QueueInitializer.InitializeQueues();
				foreach (var queue in queues)
				{
					var channel = await _connection!.GetConnection().CreateChannelAsync(cancellationToken: cancellationToken);
					_channels.Add(channel);
					await StartConsumerAsync(channel, queue.QueueName, queue.RoutingKey, cancellationToken);
				}
				#region Log
				consumerLog.Timestamp = DateTime.Now;
				consumerLog.Level = "Information";
				consumerLog.RenderedMessage = "Started consuming messages successfully";
				consumerLog.AdditionalData = $"External Data Consumer is consuming with prefetch count: [{30}]";
				_logger.LogInformation("{@LogDetails}", consumerLog);
				#endregion
			}
			catch (Exception ex)
			{
				#region Log
				consumerLog.Timestamp = DateTime.Now;
				consumerLog.Level = "Error";
				consumerLog.RenderedMessage = "Failed to Start consuming messages in external data consumer";
				consumerLog.AdditionalData = $"Error while starting channels and starting the consumption of messages in external data consumer";
				consumerLog.Exception = ex.ToString();
				consumerLog.ExceptionMessage = ex.Message;
				consumerLog.ExceptionDetail = ex.StackTrace;
				_logger.LogError(ex, "{@LogEntry}", consumerLog);
				#endregion
			}
		}
        private async Task StartConsumerAsync(IChannel channel, string queueName, string routingKey, CancellationToken cancellationToken)
        {

			var consumeBackgroundLog = new LogEntry
			{
				Category = Category.ExternalDataConsumer.ToString(),
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
				if (channel is null || !channel.IsOpen)
					throw new InvalidOperationException("Channel is not available for consuming messages.");

				await channel.BasicQosAsync(0, prefetchCount: 30, global: false);

				var consumer = new AsyncEventingBasicConsumer(channel);

				consumer.ReceivedAsync += async (sender, ea) =>
				{
					var message = Encoding.UTF8.GetString(ea.Body.ToArray());
					try
					{
						await HandleMessageAsync(ea, message, channel, cancellationToken);
						await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
					}
					catch (Exception ex)
					{
						#region Log
						consumeBackgroundLog.Timestamp = DateTime.Now;
						consumeBackgroundLog.Level = "Error";
						consumeBackgroundLog.RenderedMessage = $"Error processing message from queue";
						consumeBackgroundLog.Exception = ex.ToString();
						consumeBackgroundLog.ExceptionMessage = ex.Message;
						consumeBackgroundLog.ExceptionDetail = ex.StackTrace;
						consumeBackgroundLog.AdditionalData = $"An error occurred while processing a message from queue [{queueName}] with routing key [{routingKey}]. The message will be requeued for retry. Message content: {message}";
						#endregion
						await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true);
					}
				};

				await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer);

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
				consumeBackgroundLog.AdditionalData = $"Exception occurred while trying consume messages in External Data Consumer";
				_logger.LogCritical("{@LogDetails}", consumeBackgroundLog);
				#endregion
			}
		}
        private async Task HandleMessageAsync(BasicDeliverEventArgs ea, string message, IChannel channel, CancellationToken cancellationToken)
        {
			var handleMessageLog = new LogEntry
			{
				Category = Category.ExternalDataConsumer.ToString(),
				CategoryAction = CategoryAction.MessageHandling.ToString(),
			};
			if (cancellationToken.IsCancellationRequested)
			{
				#region Log
				handleMessageLog.Timestamp = DateTime.Now;
				handleMessageLog.Level = "Warning";
				handleMessageLog.RenderedMessage = "Cancellation requested before the start of consuming";
				handleMessageLog.AdditionalData = "Cancellation was requested before starting the consuming process. No messages will be consumed.";
				_logger.LogWarning("{@LogDetails}", handleMessageLog);
				#endregion
				return;
			}
			try
			{
				using var scope = _scopeFactory.CreateScope();
				var service = scope.ServiceProvider.GetRequiredService<IExternalDataHandlingService>();

				switch (ea.RoutingKey)
				{
					case RabbitMQConstants.AcademicQualificationRoutingKey:
						await service.AcademicDataHandle(message);
						break;
					case RabbitMQConstants.EmploymentDegreeRoutingKey:
						await service.EmploymentDataHandle(message);
						break;
					case RabbitMQConstants.ManagerialPositionRoutingKey:
						await service.ManagerialDataHandle(message);
						break;
					case RabbitMQConstants.ContactDataRoutingKey:
						await service.ContactDataHandle(message);
						break;
					case RabbitMQConstants.PersonalDataRoutingKey:
						await service.PersonalDataHandle(message);
						break;
					case RabbitMQConstants.ScientificDutyRoutingKey:
						await service.ScientificDutyDataHandle(message);
						break;
					case RabbitMQConstants.TrainingProgramRoutingKey:
						await service.TrainingProgramDataHandle(message);
						break;
					case RabbitMQConstants.ThesisSupervisionRoutingKey:
						await service.ThesisSupervisingDataHandle(message);
						break;
					case RabbitMQConstants.ThesisDataRoutingKey:
						await service.ThesisDataHandle(message);
						break;
					default:
						throw new InvalidOperationException($"Unknown routing key: {ea.RoutingKey}");
				}
				#region Log
				handleMessageLog.Timestamp = DateTime.Now;
				handleMessageLog.Level = "Information";
				handleMessageLog.RenderedMessage = $"Message processed successfully";
				handleMessageLog.AdditionalData = $"Message with routing key [{ea.RoutingKey}] was processed successfully. Message content: {message}";
				_logger.LogInformation("{@LogDetails}", handleMessageLog);
				#endregion
			}
			catch (InvalidOperationException ex)
			{
				#region Log
				handleMessageLog.Timestamp = DateTime.Now;
				handleMessageLog.Level = "Error";
				handleMessageLog.RenderedMessage = $"Failed to process message due to invalid routing key";
				handleMessageLog.Exception = ex.ToString();
				handleMessageLog.ExceptionMessage = ex.Message;
				handleMessageLog.ExceptionDetail = ex.StackTrace;
				handleMessageLog.AdditionalData = $"Message with routing key [{ea.RoutingKey}] does not match any known processing logic.";
				_logger.LogError(ex, "{@LogDetails}", handleMessageLog);
				#endregion
			}
			catch (FormatException ex)
			{
				await channel.BasicRejectAsync(ea.DeliveryTag, requeue: false);
				#region Log
				handleMessageLog.Exception = ex.ToString();
				handleMessageLog.ExceptionMessage = ex.Message;
				handleMessageLog.ExceptionDetail = ex.StackTrace;
				handleMessageLog.Timestamp = DateTime.Now;
				handleMessageLog.Level = "Error";
				handleMessageLog.RenderedMessage = $"Message rejected, Invalid message format";
				handleMessageLog.AdditionalData = $"Message with invalid format was rejected from queue with routing key : [{ea.RoutingKey}] and sent to DLQ: [{_settings.ExternalDataDLQ}] without retrying. Message content: {Encoding.UTF8.GetString(ea.Body.ToArray())}";
				_logger.LogError("{@LogDetails}", handleMessageLog);
				#endregion
			}
			catch (Exception ex)
			{
				var retryCount = GetRetryCount(ea.BasicProperties.Headers);
				if (retryCount < _settings.MaxRetryCount)
				{
					await RepublishWithRetryAsync(ea, retryCount + 1, cancellationToken);
					await channel.BasicAckAsync(ea.DeliveryTag, false);
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
					await channel.BasicRejectAsync(ea.DeliveryTag, requeue: false);
					#region Log
					handleMessageLog.Timestamp = DateTime.Now;
					handleMessageLog.Level = "Error";
					handleMessageLog.RenderedMessage = $"Message processing failed, max retries exceeded";
					handleMessageLog.Exception = ex.ToString();
					handleMessageLog.ExceptionMessage = ex.Message;
					handleMessageLog.ExceptionDetail = ex.StackTrace;
					handleMessageLog.AdditionalData = $"Processing of message failed. Max retry count of [{_settings.MaxRetryCount}] exceeded. The message will be rejected and sent to DLQ: [{_settings.ExternalDataDLQ}]. Message content: {Encoding.UTF8.GetString(ea.Body.ToArray())}";
					_logger.LogError("{@LogDetails}", handleMessageLog);
					#endregion
				}
			}
        }

		#region DLQ Helpers
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
		private async Task RepublishWithRetryAsync(BasicDeliverEventArgs ea, int retryCount, CancellationToken cancellationToken)
		{
			var republishLog = new LogEntry
			{
				Category = Category.ExternalDataConsumer.ToString(),
				CategoryAction = CategoryAction.PublishMessage.ToString(),
			};
			try
			{

				using var retryChannel = await _connection.GetConnection().CreateChannelAsync(cancellationToken: cancellationToken);
				if (retryChannel == null || !retryChannel.IsOpen)
				{
					#region Log
					republishLog.Timestamp = DateTime.Now;
					republishLog.Level = "Fatal";
					republishLog.RenderedMessage = $"Failed to recreate publisher channel for retrying message";
					republishLog.AdditionalData = $"An error occurred while trying to recreate the publisher channel for retrying the message. The message containing the external data will not be retried and will be rejected without requeuing. Message content: {Encoding.UTF8.GetString(ea.Body.ToArray())}";
					_logger.LogCritical("{@LogDetails}", republishLog);
					#endregion
					return;
				}
				var props = new BasicProperties
				{
					Persistent = true,
					Headers = new Dictionary<string, object?>
				{
					{ "data-retry-count", retryCount }
				}
				};
				await retryChannel.BasicPublishAsync(
					exchange: ea.Exchange,
					routingKey: ea.RoutingKey,
					mandatory: false,
					basicProperties: props,
					body: ea.Body);
				#region Log
				republishLog.Timestamp = DateTime.Now;
				republishLog.Level = "Information";
				republishLog.RenderedMessage = $"Message republished for retry";
				republishLog.AdditionalData = $"Message has been republished to exchange: [{ea.Exchange}] for retrying. Current retry count: [{retryCount}] out of [{_settings.MaxRetryCount}]. Message content: {Encoding.UTF8.GetString(ea.Body.ToArray())}";
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

		#endregion

	}
}
