using RabbitMQ.Client;

namespace Messaging.AsyncMessaging
{
	public interface IRabbitMQConnection
    {
        public IConnection GetConnection();
    }
}
