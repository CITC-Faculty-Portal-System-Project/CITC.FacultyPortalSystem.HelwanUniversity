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
		private IModel? _channel;
		public ResearchDataConsumerClient(IServiceScopeFactory serviceScope, 
			IRabbitMQConnection connection,
			 IOptions<RabbitMQSettings> options)
		{
			_settings = options.Value;
			_serviceScope = serviceScope;
			_connection = connection;
		}
		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_channel = _connection.GetConnection().CreateModel();
			_channel.QueueDeclare(
				queue: _settings.ResearchDataQueueName,
				durable: true,
				exclusive: false,
				autoDelete: false,
				arguments: null);

			_channel.QueueBind(queue: _settings.ResearchDataQueueName, exchange: _settings.ResearchDataExchangeName, routingKey: _settings.ResearchDataRoutingKey);

			var consumer = new AsyncEventingBasicConsumer(_channel);

			consumer.Received += async (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);

				using var scope = _serviceScope.CreateScope();
				var service = scope.ServiceProvider.GetRequiredService<IExternalDataHandlingService>();
				await service.ResearchDataHandle(message);
				_channel.BasicAck(ea.DeliveryTag, false);
			};

			_channel.BasicConsume(
				queue: _settings.ResearchDataQueueName,
				autoAck: false,
				consumer: consumer);

			return Task.CompletedTask;
		}

		public override void Dispose()
		{
			try
			{
				if(_channel is not null)
				{
					if (_channel.IsOpen)
						_channel.Close();
					_channel.Dispose();
				}
			}catch(Exception ex)
			{
				Console.WriteLine($"--> Failed to dispose channel: {ex.Message}");
			}
			base.Dispose();
		}
	}
}
