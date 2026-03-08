using Serilog.Events;
using Serilog.Formatting;
using Shared;
using System.Text.Json;

namespace ICIT.FacultyPortalSystem.API.Logger
{
	public class CustomLogFormatter(IHttpContextAccessor _httpContextAccessor, IServiceScopeFactory _scopeFactory) : ITextFormatter
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
				UserName = string.IsNullOrWhiteSpace(GetLogDetailProperty(logEvent, "UserName")) ? null : UserNameResolver(),
				UserIP = string.IsNullOrWhiteSpace(GetLogDetailProperty(logEvent, "UserIP")) ? null : IPResolver(),
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
		private string? IPResolver()
		{
			var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress;
			if (ip != null)
			{
				if (ip.IsIPv4MappedToIPv6)
					ip = ip.MapToIPv4();
			}
			return ip?.ToString();
		}
		private string? UserNameResolver()
		{
			using var scope = _scopeFactory.CreateScope();

			var authService = scope.ServiceProvider
				.GetRequiredService<IAuthenticationService>();

			var email = authService.GetLoggedUserEmail();
			if (string.IsNullOrWhiteSpace(email))
				return null;

			var user =  authService.GetCurrentUserAsync(email);
			return user?.Result?.UserName;
		}

	}
}
