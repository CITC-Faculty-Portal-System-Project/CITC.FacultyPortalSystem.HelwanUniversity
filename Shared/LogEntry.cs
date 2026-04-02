namespace Shared
{
	public class LogEntry
    {
		public DateTime Timestamp { get; set; }
		public string Level { get; set; } = string.Empty;
		public string RenderedMessage { get; set; } = string.Empty;
		public string Category { get; set; } = string.Empty;
		public string CategoryAction { get; set; } = string.Empty;
		public string? UserName { get; set; }
		public string? UserIP { get; set; }
		public string? Exception { get; set; }
		public string? ExceptionMessage { get; set; }
		public string? ExceptionDetail { get; set; }
		public string? AdditionalData { get; set; }
		public string Code { get; init; } = "2001"; //[Do not change!] Code responsible for identifying the log entry type [Source], can be used for filtering and categorization.
	}
}
