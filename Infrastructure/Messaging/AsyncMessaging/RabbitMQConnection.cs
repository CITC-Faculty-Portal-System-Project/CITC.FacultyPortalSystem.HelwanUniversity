using Messaging.AsyncMessaging.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.AsyncMessaging
{
    public class RabbitMQConnection : IRabbitMQConnection, IDisposable
    {
        private IConnection? _connection;
        private readonly RabbitMQConsumerSettings _settings;
        public RabbitMQConnection(IOptions<RabbitMQConsumerSettings> options)
        {
            _settings = options.Value;
            var factory = new ConnectionFactory
            {
                HostName = _settings.Host,
                Port = _settings.Port,
                UserName = _settings.Username,
                Password = _settings.Password,
				DispatchConsumersAsync = true 
			};
            _connection = factory.CreateConnection();
        }
        public IConnection GetConnection() => _connection ?? throw new InvalidOperationException("RabbitMQ connection is not established.");

        public void Dispose()
        {
            if (_connection != null)
            {
                if (_connection.IsOpen)
                    _connection.Close();
                _connection.Dispose();
            }
        }
    }
}
