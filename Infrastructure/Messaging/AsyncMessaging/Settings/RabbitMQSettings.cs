namespace Messaging.AsyncMessaging.Settings
{
	public class RabbitMQSettings
	{
		//These are default values; they can be overridden by configuration
		#region NationalNumberPubClient
		public string NationalNumberExchangeName { get; set; } = "nationalNumber_exchange";
		#endregion
		#region ResearchDataConsumerClient
		public string ResearchDataExchangeName { get; set; } = "external.researches.exchange";
		public string ResearchDataRoutingKey { get; set; } = "external.researches.fetch";
		public string ResearchDataQueueName { get; set; } = "external.researches.queue";
		#endregion
	}
}
