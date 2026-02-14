using Messaging.AsyncMessaging.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Services.Abstraction.Contracts;
using System.Text;

namespace Messaging.AsyncMessaging.Publisher
{
	public class NationalNumberPubClient : INationalNumberPubClient, IAsyncDisposable 
	{
		private readonly IRabbitMQConnection _connection;
		private readonly RabbitMQSettings _settings;
		private readonly IChannel _channel;
		private bool _disposed;

		public NationalNumberPubClient(IRabbitMQConnection rabbitMQConnection, IOptions<RabbitMQSettings> options)
		{
			_settings = options.Value;
			_connection = rabbitMQConnection;

			try
			{
				_channel = _connection.GetConnection().CreateChannelAsync().GetAwaiter().GetResult(); // Create channel synchronously suitable for DI

			}
			catch (Exception ex)
			{
				Console.WriteLine($"--> Could not create RabbitMQ connection: {ex.Message}");
				throw new InvalidOperationException("Could Not Connect with RabbitMQ");
			}

		}

		public async Task PublishUserNationalNumberAsync(string nationalNumber)
		{
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

				Console.WriteLine($"--> Published national number: {nationalNumber}");
			}catch(Exception ex)
			{
				Console.WriteLine($"--> Could not publish message: {ex.Message}");
			}

		}

		public async ValueTask DisposeAsync()
		{
			if (_disposed) return;

			try
			{
				if (_channel != null)
					await _channel.CloseAsync();

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
