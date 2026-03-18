using Messaging.Kafka;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace ICIT.FacultyPortalSystem.API.Logger
{
	public class KafkaLogSink : ILogEventSink
	{

		private readonly KafkaLogPublisher _publisher;
		private readonly ITextFormatter _formatter;

		public KafkaLogSink(ITextFormatter? formatter = null)
		{

			_formatter = formatter ?? new CustomLogFormatter();
			_publisher = new KafkaLogPublisher();
		}
		public async void Emit(LogEvent logEvent)
		{
			try
			{
				using var writer = new StringWriter();
				_formatter.Format(logEvent, writer);
				if (writer is not null)
				{
					var formattedLog = writer.ToString();
					if (!string.IsNullOrWhiteSpace(formattedLog))
						await _publisher.PublishAsync(null, formattedLog);
				}
			}

			catch (Exception ex)
			{
				Console.WriteLine($"[KafkaSink] Failed to publish log: {ex.Message}");
			}
		}
	}
}
