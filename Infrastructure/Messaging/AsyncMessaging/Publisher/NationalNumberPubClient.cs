using Messaging.AsyncMessaging.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Services.Abstraction.Contracts;
using System.Text;

namespace Messaging.AsyncMessaging.Publisher
{
	public class NationalNumberPubClient : INationalNumberPubClient, IDisposable
	{
		private readonly IRabbitMQConnection _connection;
		private readonly RabbitMQSettings _settings;
		private readonly IModel _channel;
		private bool _disposed;

		public NationalNumberPubClient(IRabbitMQConnection rabbitMQConnection, IOptions<RabbitMQSettings> options)
		{
			_settings = options.Value;
			_connection = rabbitMQConnection;

			try
			{
				_channel = _connection.GetConnection().CreateModel();
				_channel.ExchangeDeclare(_settings.NationalNumberExchangeName, ExchangeType.Fanout, durable: true);

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
				if (_channel is null || !_channel.IsOpen || !_connection.GetConnection().IsOpen)
					throw new InvalidOperationException("RabbitMQ channel is closed or unavailable.");


				var body = Encoding.UTF8.GetBytes(nationalNumber);

				var props = _channel.CreateBasicProperties();
				props.Persistent = true;

				_channel.BasicPublish(
					exchange: _settings.NationalNumberExchangeName,
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
				_channel?.Dispose();
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
