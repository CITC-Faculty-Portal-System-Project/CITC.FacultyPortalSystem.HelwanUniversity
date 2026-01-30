using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services.Abstraction.Contracts;
using System.Text;
using System.Threading.Channels;

namespace Messaging.AsyncMessaging.Consumer
{
	public class ResearchDataConsumerClient : BackgroundService
	{
		private readonly IServiceScopeFactory _serviceScope;
		private readonly IRabbitMQConnection _connection;
		private IModel? _channel;
		public ResearchDataConsumerClient(IServiceScopeFactory serviceScope, IRabbitMQConnection connection)
		{
			_serviceScope = serviceScope;
			_connection = connection;
		}
		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_channel = _connection.GetConnection().CreateModel();
			_channel.QueueDeclare(
				queue: "external.researches.queue",
				durable: true,
				exclusive: false,
				autoDelete: false,
				arguments: null);

			_channel.QueueBind(queue: "external.researches.queue", exchange: "external.researches.exchange", routingKey: "external.researches.fetch");

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
				queue: "external.researches.queue",
				autoAck: false,
				consumer: consumer);

			return Task.CompletedTask;
		}

		public override void Dispose()
		{
			_channel?.Close();
			_channel?.Dispose();
			base.Dispose();
		}
	}
}
