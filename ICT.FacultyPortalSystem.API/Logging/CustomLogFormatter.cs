using Serilog.Events;
using Serilog.Formatting;
using Shared;
using System.Text.Json;

namespace ICIT.FacultyPortalSystem.API.Logger
{
	public class CustomLogFormatter : ITextFormatter
	{ 
		public void Format(LogEvent logEvent, TextWriter output)
		{
			var logEntry = new LogEntry
			{
				Timestamp = logEvent.Timestamp.DateTime.ToLocalTime(),
				Level = logEvent.Level.ToString(),
				RenderedMessage = GetLogDetailProperty(logEvent, "RenderedMessage") ?? string.Empty,
				Category = GetLogDetailProperty(logEvent, "Category") ?? string.Empty,
				CategoryAction = GetLogDetailProperty(logEvent, "CategoryAction") ?? string.Empty,
				UserName = GetLogDetailProperty(logEvent, "UserName"),
				UserIP = GetLogDetailProperty(logEvent, "UserIP"),
				Exception = GetLogDetailProperty(logEvent, "Exception"),
				ExceptionMessage = GetLogDetailProperty(logEvent, "ExceptionMessage"),
				ExceptionDetail = GetLogDetailProperty(logEvent, "ExceptionDetail"),
				AdditionalData = GetLogDetailProperty(logEvent, "AdditionalData"),
				Code = "2001" // [Do not change!] Code responsible for identifying the log entry type [Source], can be used for filtering and categorization.
			};
			var json = JsonSerializer.Serialize(logEntry);
			if (json is not null && !string.IsNullOrWhiteSpace(logEntry.RenderedMessage))
				output.Write(json);
		}
		private string GetLogDetailProperty(LogEvent logEvent, string name)
		{
			if (logEvent.Properties.TryGetValue("LogDetails", out var details))
			{
				// Serilog stores destructured objects as StructureValue
				if (details is StructureValue structure)
				{
					var prop = structure.Properties.FirstOrDefault(p => p.Name == name);
					return prop?.Value.ToString().Trim('"') ?? string.Empty;
				}
			}
			return string.Empty;
		}
	}
}
