using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Services.Abstraction.Contracts;
using System.Text;

namespace Messaging.AsyncMessaging.Publisher
{
	public class NationalNumberPubClient : INationalNumberPubClient, IDisposable
	{
		private readonly IConnection _connection;
		private readonly IModel _channel;
		private readonly string _exchangeName = "data_exchange";
		private readonly string _queueName = "NationalNumber-queue";
		private readonly string _routingKey = "nationalNumberRK";

		public NationalNumberPubClient(IConfiguration config)
		{
			var factory = new ConnectionFactory
			{
				HostName = config["RabbitMQ:Host"] ?? "localhost",
				Port = int.Parse(config["RabbitMQ:Port"] ?? "5672"),
				UserName = config["RabbitMQ:Username"] ?? "guest",
				Password = config["RabbitMQ:Password"] ?? "guest"
			};

			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();

			_channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct, durable: true);
			_channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);
			_channel.QueueBind(_queueName, _exchangeName, _routingKey);
		}

		public void PublishUserNationalNumber(string nationalNumber)
		{
			var body = Encoding.UTF8.GetBytes(nationalNumber);
			_channel.BasicPublish(
				exchange: _exchangeName,
				routingKey: _routingKey,
				basicProperties: null,
				body: body
			);

			Console.WriteLine($"--> Published national number: {nationalNumber}");
		}

		public void Dispose()
		{
			_channel?.Close();
			_connection?.Close();
		}
	}
}
