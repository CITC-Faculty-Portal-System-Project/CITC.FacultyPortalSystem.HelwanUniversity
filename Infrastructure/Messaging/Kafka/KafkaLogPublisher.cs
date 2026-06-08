using Confluent.Kafka;

namespace Messaging.Kafka
{
	public class KafkaLogPublisher
	{
		private readonly IProducer<string?, string> _producer;
		private readonly string _topic;

		public KafkaLogPublisher(string bootstrapServers = "kafka:9092", string topic = "logs-topic")
		{
			var config = new ProducerConfig
			{
				BootstrapServers = bootstrapServers,
				Acks = Acks.All
			};
			_topic = topic;
			_producer = new ProducerBuilder<string?, string>(config).Build();
		}

		public async Task PublishAsync(string? key, string message) //key can be used to partition messages, e.g., by log level or source to ensure ordering in each partition
		{
			await _producer.ProduceAsync(_topic, new Message<string?, string>
			{
				Key = key,
				Value = message
			});
		}
	}
}
