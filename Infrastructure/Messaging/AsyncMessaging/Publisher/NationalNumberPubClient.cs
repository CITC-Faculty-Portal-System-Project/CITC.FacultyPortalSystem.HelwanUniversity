using Messaging.AsyncMessaging.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Services.Abstraction.Contracts;
using System.Text;

namespace Messaging.AsyncMessaging.Publisher
{
	public class NationalNumberPubClient : INationalNumberPubClient, IDisposable
	{
		private readonly IConnection _connection;
		private readonly IModel _channel;
		private readonly RabbitMQPublishSettings _settings;
		private bool _disposed;

		public NationalNumberPubClient(IOptions<RabbitMQPublishSettings> options)
		{
			_settings = options.Value;

			try
			{
				var factory = new ConnectionFactory
				{
					HostName = _settings.Host,
					Port = _settings.Port,
					UserName = _settings.Username,
					Password = _settings.Password,
					AutomaticRecoveryEnabled = true, // auto-reconnect if RabbitMQ restarts
					NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
				};

				_connection = factory.CreateConnection();
				_channel = _connection.CreateModel();

				_channel.ExchangeDeclare(_settings.ExchangeName, ExchangeType.Fanout, durable: true);

			}catch (Exception ex)
			{
				Console.WriteLine($"--> Could not create RabbitMQ connection: {ex.Message}");
				throw new InvalidOperationException("Could Not Connect with RabbitMQ"); //==> Exception Handler
			}

		}

		public void PublishUserNationalNumber(string nationalNumber)
		{

			try
			{
				if (_channel is null || !_channel.IsOpen || _disposed)
					throw new InvalidOperationException("RabbitMQ channel is closed or unavailable.");


				var body = Encoding.UTF8.GetBytes(nationalNumber);

				var props = _channel.CreateBasicProperties();
				props.Persistent = true;

				_channel.BasicPublish(
					exchange: _settings.ExchangeName,
					routingKey: "",
					basicProperties: props, // make message persistent
					body: body
				);

				Console.WriteLine($"--> Published national number: {nationalNumber}");
			}catch(Exception ex)
			{
				Console.WriteLine($"--> Could not publish message: {ex.Message}");
			}

		}

		public void Dispose()
		{
			if (_disposed) return;
			try
			{
				_channel?.Close();
				_connection?.Close();
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
