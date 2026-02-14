using Messaging.AsyncMessaging.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services.Abstraction.Contracts;
using System.Text;

namespace Messaging.AsyncMessaging.Consumer
{
	public class ResearchDataConsumerClient : BackgroundService
	{
		private readonly IServiceScopeFactory _serviceScope;
		private readonly IRabbitMQConnection _connection;
		private readonly RabbitMQSettings _settings;
		private IChannel? _channel;
		private IChannel? _publisherChannel;
		public ResearchDataConsumerClient(IServiceScopeFactory serviceScope,
			IRabbitMQConnection connection,
			 IOptions<RabbitMQSettings> options)
		{
			_settings = options.Value;
			_serviceScope = serviceScope;
			_connection = connection;
		}

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			try
			{
				await InitializeRabbitMQAsync(cancellationToken);
				await StartConsumingAsync(cancellationToken);
				await Task.Delay(Timeout.Infinite, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				// Graceful shutdown
			}
			catch (Exception ex)
			{
				Console.WriteLine($"--> Initialization error: {ex.Message}");
			}
		}

		public override async Task StopAsync(CancellationToken cancellationToken)
		{

			if (_channel is not null)
			{
				try
				{
					if (_channel.IsOpen)
						await _channel.CloseAsync(cancellationToken: cancellationToken);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"--> Failed to close channel: {ex.Message}");
				}
			}
			if (_publisherChannel is not null)
			{
				try
				{
					if (_publisherChannel.IsOpen)
						await _publisherChannel.CloseAsync(cancellationToken: cancellationToken);
				}
				catch
				(Exception ex)
				{
					Console.WriteLine($"--> Failed to close publisher channel: {ex.Message}");
				}
			}

		}

		public override void Dispose()
		{
			try
			{
				_channel?.Dispose();
				_publisherChannel?.Dispose();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"--> Failed to dispose channel: {ex.Message}");
			}

			base.Dispose();
		}

		#region Initialization Methods

		private async Task InitializeRabbitMQAsync(CancellationToken cancellationToken)
		{
			_channel = await _connection.GetConnection().CreateChannelAsync(cancellationToken: cancellationToken);
			_publisherChannel = await _connection.GetConnection().CreateChannelAsync(cancellationToken: cancellationToken);
			await DeclareExchangeAndQueueAsync(cancellationToken);
			Console.WriteLine($"---> Connected to RabbitMQ");
		}
		private async Task DeclareExchangeAndQueueAsync(CancellationToken cancellationToken)
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

		}

		#endregion

		#region Consumers

		private async Task StartConsumingAsync(CancellationToken cancellationToken)
		{
			if(cancellationToken.IsCancellationRequested)
			{
				//might nack
				Console.WriteLine("--> Cancellation requested before starting consumer.");
				return;
			}
			if(_channel is null || !_channel.IsOpen)
			{
				Console.WriteLine("--> Channel is not available for consuming.");
				return;
			}

			await _channel!.BasicQosAsync(0, prefetchCount: 30, global: false);

			try
			{
				var consumer = new AsyncEventingBasicConsumer(_channel!);
				consumer.ReceivedAsync += HandleMessageAsync;

				await _channel!.BasicConsumeAsync(
					queue: _settings.ResearchDataQueueName,
					autoAck: false,
					consumer: consumer,
					cancellationToken: cancellationToken);

			}
			catch (Exception ex)
			{
				Console.WriteLine($"--> Error in StartConsumingAsync: {ex.Message}");
			}

		}

		private async Task HandleMessageAsync(object sender, BasicDeliverEventArgs ea)
		{

			try
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);

				using var scope = _serviceScope.CreateScope();
				var service = scope.ServiceProvider.GetRequiredService<IExternalDataHandlingService>();
				await service.ResearchDataHandle(message);
				await _channel!.BasicAckAsync(ea.DeliveryTag, false);
			}
			catch (FormatException)
			{
				await _channel!.BasicRejectAsync(ea.DeliveryTag, requeue: false);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"--> Processing failed: {ex.Message}");

				var retryCount = GetRetryCount(ea.BasicProperties.Headers);

				if (retryCount < _settings.MaxRetryCount)
				{
					await RepublishWithRetryAsync(ea, retryCount + 1);
					await _channel!.BasicAckAsync(ea.DeliveryTag, false);
				}
				else
				{
					Console.WriteLine("--> Max retries exceeded → DLQ");
					await _channel!.BasicRejectAsync(ea.DeliveryTag, requeue: false);
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

			if (_publisherChannel == null || !_publisherChannel.IsOpen)
			{
				try
				{
					_publisherChannel = await _connection.GetConnection().CreateChannelAsync(cancellationToken: default);
				}
				catch (Exception)
				{
					Console.WriteLine("--> Failed to recreate publisher channel.");
					return;
				}
			}

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

			Console.WriteLine($"--> Retry {retryCount}/{_settings.MaxRetryCount}");

		}
		#endregion
	}
}
