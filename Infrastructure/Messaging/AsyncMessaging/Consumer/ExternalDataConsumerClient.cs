using Messaging.AsyncMessaging.Consumer.Helpers;
using Messaging.AsyncMessaging.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services.Abstraction.Contracts;
using System.Collections.Concurrent;
using System.Text;

namespace Messaging.AsyncMessaging.Consumer
{
	public class ExternalDataConsumerClient : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IRabbitMQConnection _connection;
		private readonly RabbitMQSettings _settings;
		private readonly ConcurrentBag<IChannel> _channels = new();

        public ExternalDataConsumerClient(
            IRabbitMQConnection rabbitMQConnection, 
            IServiceScopeFactory scopeFactory,
			IOptions<RabbitMQSettings> options)
        {
			_connection = rabbitMQConnection;
			_scopeFactory = scopeFactory;
			_settings = options.Value;

		}

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await InitializeRabbitMQAsync(cancellationToken);
                await base.StartAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"==> Error during RabbitMQ Initialization: {ex.Message}");
			}
        }

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			try
			{ 
					await StartConsumersAsync(stoppingToken);
					await Task.Delay(Timeout.Infinite, stoppingToken);
			}
			catch (OperationCanceledException)
			{
				//> Expected during shutdown, no action needed.
			}
			catch (Exception ex)
			{
				Console.WriteLine($"==> Error during starting consumers: {ex.Message}");
				throw;
			}
		}

		public override async Task StopAsync(CancellationToken cancellationToken)
		{
			if (_channels.Any())
			{
				try
				{
					foreach (var channel in _channels)
					{ 
						if (channel.IsOpen) 
							await channel.CloseAsync(cancellationToken: cancellationToken); 
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"--> Failed to close channels: {ex.Message}");
				}
			}
		}

		public override void Dispose()
		{
			if (_channels.Any())
			{
				Console.WriteLine("--> Disposing consumer channels...");

				foreach (var channel in _channels)
				{
					try
					{
						channel.Dispose();
					}
					catch (Exception ex)
					{
						Console.WriteLine($"--> Failed to dispose channel: {ex.Message}");
					}
				}
				Console.WriteLine("--> Consumer disposed (connection kept alive).");
			}
			base.Dispose();
		}


		#region Helpers
		private async Task InitializeRabbitMQAsync(CancellationToken cancellationToken)
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

		}
        private async Task StartConsumersAsync(CancellationToken cancellationToken)
        {
            var queues = QueueInitializer.InitializeQueues();

            foreach(var queue in queues)
            {
				var channel = await _connection!.GetConnection().CreateChannelAsync(cancellationToken: cancellationToken);
				_channels.Add(channel);
				await StartConsumerAsync(channel, queue.QueueName,queue.RoutingKey, cancellationToken);
			}
		}
        private async Task StartConsumerAsync(IChannel channel, string queueName, string routingKey, CancellationToken cancellationToken)
        {
			if (channel is null || !channel.IsOpen)
			{
				Console.WriteLine("--> Channel is not available for processing.");
				return;
			}

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
					Console.WriteLine($"==> Error processing message from '{queueName}': {ex.Message}");
					await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true);
				}
			};

			await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer);
		}
        private async Task HandleMessageAsync(BasicDeliverEventArgs ea, string message, IChannel channel, CancellationToken cancellationToken)
        {
			if(cancellationToken.IsCancellationRequested)
			{
				//might nack
				Console.WriteLine("--> Cancellation requested before processing message.");
				return;
			}
			try
			{
				using var scope = _scopeFactory.CreateScope();
				var service = scope.ServiceProvider.GetRequiredService<IExternalDataHandlingService>();

				switch (ea.RoutingKey)
				{
					case RabbitMQConstants.AcademicQualificationRoutingKey:
						//throw new FormatException("Simulated format error for testing DLQ.");
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
						//throw new FormatException("Simulated format error for testing DLQ.");
						await service.PersonalDataHandle(message);
						break;
					case RabbitMQConstants.ScientificDutyRoutingKey:
						await service.ScientificDutyDataHandle(message);
						break;
					case RabbitMQConstants.TrainingProgramRoutingKey:
						await service.TrainingProgramDataHandle(message);
						break;
					case RabbitMQConstants.ThesisSupervisionRoutingKey:
						//throw new FormatException("Simulated format error for testing DLQ.");
						await service.ThesisSupervisingDataHandle(message);
						break;
					case RabbitMQConstants.ThesisDataRoutingKey:
						//throw new FormatException("Simulated format error for testing DLQ.");
						await service.ThesisDataHandle(message);
						break;
					default:
						throw new InvalidOperationException($"Unknown routing key: {ea.RoutingKey}");
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine($"==> Routing error: {ex.Message}");
			}
			catch (FormatException)
			{
				await channel.BasicRejectAsync(ea.DeliveryTag, requeue: false);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"--> Processing failed: {ex.Message}");

				var retryCount = GetRetryCount(ea.BasicProperties.Headers);

				if (retryCount < _settings.MaxRetryCount)
				{
					await RepublishWithRetryAsync(ea, retryCount + 1, cancellationToken);
					await channel.BasicAckAsync(ea.DeliveryTag, false);
					Console.WriteLine($"Re-Published for message {message}");
				}
				else
				{
					Console.WriteLine($"--> Max retries exceeded → DLQ [Message : {message}]");
					await channel.BasicRejectAsync(ea.DeliveryTag, requeue: false);
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
			using var retryChannel = await _connection.GetConnection().CreateChannelAsync(cancellationToken: cancellationToken);

			if (retryChannel == null || !retryChannel.IsOpen)
			{
				Console.WriteLine("--> Failed to create retry channel.");
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



			Console.WriteLine($"--> Retry {retryCount}/{_settings.MaxRetryCount}");

		}

		#endregion

		#endregion

	}
}
