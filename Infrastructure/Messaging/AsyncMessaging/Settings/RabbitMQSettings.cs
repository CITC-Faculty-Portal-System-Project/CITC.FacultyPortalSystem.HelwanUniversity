namespace Messaging.AsyncMessaging.Settings
{
	public class RabbitMQSettings
	{
		public readonly double TimeoutSeconds = 200;
		public readonly int MaxRetryCount = 5;
		//These are default values; they can be overridden by configuration
		#region NationalNumberPubClient
		public string NationalNumberExchangeName { get; set; } = "nationalNumber_exchange";
		#endregion
		#region ResearchDataConsumerClient
		public string ResearchDataExchangeName { get; set; } = "external.researches.exchange";
		public string ResearchDataRoutingKey { get; set; } = "external.researches.fetch";
		public string ResearchDataQueueName { get; set; } = "external.researches.queue";

		//DLQ:
		public string ResearchDataDLX { get; set; } = "external.researches.dlx";
		public string ResearchDataDLQ { get; set; } = "external.researches.dlq";
		public string ResearchDataDLRK { get; set; } = "external.researches.RK";

		#endregion
		#region ExternalDataConsumerClient
		//DLQ:
		public string ExternalDataDLX { get; set; } = "external.data.dlx";
		public string ExternalDataDLQ { get; set; } = "external.data.dlq";
		public string ExternalDataDLRK { get; set; } = "external.data.RK";
		#endregion
	}
}
